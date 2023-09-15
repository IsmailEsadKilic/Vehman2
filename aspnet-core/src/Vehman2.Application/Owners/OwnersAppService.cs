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
using Vehman2.Owners;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;
using Vehman2.Shared;

namespace Vehman2.Owners
{
    [RemoteService(IsEnabled = false)]
    [Authorize(Vehman2Permissions.Owners.Default)]
    public class OwnersAppService : ApplicationService, IOwnersAppService
    {
        private readonly IDistributedCache<OwnerExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;
        private readonly IOwnerRepository _ownerRepository;
        private readonly OwnerManager _ownerManager;

        public OwnersAppService(IOwnerRepository ownerRepository, OwnerManager ownerManager, IDistributedCache<OwnerExcelDownloadTokenCacheItem, string> excelDownloadTokenCache)
        {
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _ownerRepository = ownerRepository;
            _ownerManager = ownerManager;
        }

        public virtual async Task<PagedResultDto<OwnerDto>> GetListAsync(GetOwnersInput input)
        {
            var totalCount = await _ownerRepository.GetCountAsync(input.FilterText, input.Name);
            var items = await _ownerRepository.GetListAsync(input.FilterText, input.Name, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<OwnerDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Owner>, List<OwnerDto>>(items)
            };
        }

        public virtual async Task<OwnerDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<Owner, OwnerDto>(await _ownerRepository.GetAsync(id));
        }

        [Authorize(Vehman2Permissions.Owners.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _ownerRepository.DeleteAsync(id);
        }

        [Authorize(Vehman2Permissions.Owners.Create)]
        public virtual async Task<OwnerDto> CreateAsync(OwnerCreateDto input)
        {

            var owner = await _ownerManager.CreateAsync(
            input.Name
            );

            return ObjectMapper.Map<Owner, OwnerDto>(owner);
        }

        [Authorize(Vehman2Permissions.Owners.Edit)]
        public virtual async Task<OwnerDto> UpdateAsync(Guid id, OwnerUpdateDto input)
        {

            var owner = await _ownerManager.UpdateAsync(
            id,
            input.Name, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<Owner, OwnerDto>(owner);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(OwnerExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var items = await _ownerRepository.GetListAsync(input.FilterText, input.Name);

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(ObjectMapper.Map<List<Owner>, List<OwnerExcelDto>>(items));
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "Owners.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new OwnerExcelDownloadTokenCacheItem { Token = token },
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