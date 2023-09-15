using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Vehman2.Permissions;
using Vehman2.Brands;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;
using Vehman2.Shared;

namespace Vehman2.Brands
{
    [RemoteService(IsEnabled = false)]
    [Authorize(Vehman2Permissions.Brands.Default)]
    public class BrandsAppService : ApplicationService, IBrandsAppService
    {
        private readonly IDistributedCache<BrandExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;
        private readonly IBrandRepository _brandRepository;
        private readonly BrandManager _brandManager;

        public BrandsAppService(IBrandRepository brandRepository, BrandManager brandManager, IDistributedCache<BrandExcelDownloadTokenCacheItem, string> excelDownloadTokenCache)
        {
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _brandRepository = brandRepository;
            _brandManager = brandManager;
        }

        public virtual async Task<PagedResultDto<BrandDto>> GetListAsync(GetBrandsInput input)
        {
            var totalCount = await _brandRepository.GetCountAsync(input.FilterText, input.Name);
            var items = await _brandRepository.GetListAsync(input.FilterText, input.Name, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<BrandDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Brand>, List<BrandDto>>(items)
            };
        }

        public virtual async Task<BrandDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<Brand, BrandDto>(await _brandRepository.GetAsync(id));
        }

        [Authorize(Vehman2Permissions.Brands.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _brandRepository.DeleteAsync(id);
        }

        [Authorize(Vehman2Permissions.Brands.Create)]
        public virtual async Task<BrandDto> CreateAsync(BrandCreateDto input)
        {

            var brand = await _brandManager.CreateAsync(
            input.Name
            );

            return ObjectMapper.Map<Brand, BrandDto>(brand);
        }

        [Authorize(Vehman2Permissions.Brands.Edit)]
        public virtual async Task<BrandDto> UpdateAsync(Guid id, BrandUpdateDto input)
        {

            var brand = await _brandManager.UpdateAsync(
            id,
            input.Name, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<Brand, BrandDto>(brand);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(BrandExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var items = await _brandRepository.GetListAsync(input.FilterText, input.Name);

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(ObjectMapper.Map<List<Brand>, List<BrandExcelDto>>(items));
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "Brands.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new BrandExcelDownloadTokenCacheItem { Token = token },
                new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30)
                });

            return new DownloadTokenResultDto
            {
                Token = token
            };
        }
    }
}