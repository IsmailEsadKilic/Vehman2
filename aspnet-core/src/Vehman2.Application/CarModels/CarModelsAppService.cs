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
using Vehman2.CarModels;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;
using Vehman2.Shared;

namespace Vehman2.CarModels
{
    [RemoteService(IsEnabled = false)]
    [Authorize(Vehman2Permissions.CarModels.Default)]
    public class CarModelsAppService : ApplicationService, ICarModelsAppService
    {
        private readonly IDistributedCache<CarModelExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;
        private readonly ICarModelRepository _carModelRepository;
        private readonly CarModelManager _carModelManager;

        public CarModelsAppService(ICarModelRepository carModelRepository, CarModelManager carModelManager, IDistributedCache<CarModelExcelDownloadTokenCacheItem, string> excelDownloadTokenCache)
        {
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _carModelRepository = carModelRepository;
            _carModelManager = carModelManager;
        }

        public virtual async Task<PagedResultDto<CarModelDto>> GetListAsync(GetCarModelsInput input)
        {
            var totalCount = await _carModelRepository.GetCountAsync(input.FilterText, input.Name);
            var items = await _carModelRepository.GetListAsync(input.FilterText, input.Name, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<CarModelDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<CarModel>, List<CarModelDto>>(items)
            };
        }

        public virtual async Task<CarModelDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<CarModel, CarModelDto>(await _carModelRepository.GetAsync(id));
        }

        [Authorize(Vehman2Permissions.CarModels.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _carModelRepository.DeleteAsync(id);
        }

        [Authorize(Vehman2Permissions.CarModels.Create)]
        public virtual async Task<CarModelDto> CreateAsync(CarModelCreateDto input)
        {

            var carModel = await _carModelManager.CreateAsync(
            input.Name
            );

            return ObjectMapper.Map<CarModel, CarModelDto>(carModel);
        }

        [Authorize(Vehman2Permissions.CarModels.Edit)]
        public virtual async Task<CarModelDto> UpdateAsync(Guid id, CarModelUpdateDto input)
        {

            var carModel = await _carModelManager.UpdateAsync(
            id,
            input.Name, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<CarModel, CarModelDto>(carModel);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(CarModelExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var items = await _carModelRepository.GetListAsync(input.FilterText, input.Name);

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(ObjectMapper.Map<List<CarModel>, List<CarModelExcelDto>>(items));
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "CarModels.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new CarModelExcelDownloadTokenCacheItem { Token = token },
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