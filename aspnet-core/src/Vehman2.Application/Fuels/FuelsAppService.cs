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
using Vehman2.Fuels;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;
using Vehman2.Shared;

namespace Vehman2.Fuels
{
    [RemoteService(IsEnabled = false)]
    [Authorize(Vehman2Permissions.Fuels.Default)]
    public class FuelsAppService : ApplicationService, IFuelsAppService
    {
        private readonly IDistributedCache<FuelExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;
        private readonly IFuelRepository _fuelRepository;
        private readonly FuelManager _fuelManager;

        public FuelsAppService(IFuelRepository fuelRepository, FuelManager fuelManager, IDistributedCache<FuelExcelDownloadTokenCacheItem, string> excelDownloadTokenCache)
        {
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _fuelRepository = fuelRepository;
            _fuelManager = fuelManager;
        }

        public virtual async Task<PagedResultDto<FuelDto>> GetListAsync(GetFuelsInput input)
        {
            var totalCount = await _fuelRepository.GetCountAsync(input.FilterText, input.Name);
            var items = await _fuelRepository.GetListAsync(input.FilterText, input.Name, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<FuelDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Fuel>, List<FuelDto>>(items)
            };
        }

        public virtual async Task<FuelDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<Fuel, FuelDto>(await _fuelRepository.GetAsync(id));
        }

        [Authorize(Vehman2Permissions.Fuels.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _fuelRepository.DeleteAsync(id);
        }

        [Authorize(Vehman2Permissions.Fuels.Create)]
        public virtual async Task<FuelDto> CreateAsync(FuelCreateDto input)
        {

            var fuel = await _fuelManager.CreateAsync(
            input.Name
            );

            return ObjectMapper.Map<Fuel, FuelDto>(fuel);
        }

        [Authorize(Vehman2Permissions.Fuels.Edit)]
        public virtual async Task<FuelDto> UpdateAsync(Guid id, FuelUpdateDto input)
        {

            var fuel = await _fuelManager.UpdateAsync(
            id,
            input.Name, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<Fuel, FuelDto>(fuel);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(FuelExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var items = await _fuelRepository.GetListAsync(input.FilterText, input.Name);

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(ObjectMapper.Map<List<Fuel>, List<FuelExcelDto>>(items));
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "Fuels.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new FuelExcelDownloadTokenCacheItem { Token = token },
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