using Vehman2.Shared;
using Vehman2.Owners;
using Vehman2.Fuels;
using Vehman2.CarModels;
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
using Vehman2.Vehicles;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;
using Vehman2.Shared;

namespace Vehman2.Vehicles
{
    [RemoteService(IsEnabled = false)]
    [Authorize(Vehman2Permissions.Vehicles.Default)]
    public class VehiclesAppService : ApplicationService, IVehiclesAppService
    {
        private readonly IDistributedCache<VehicleExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;
        private readonly IVehicleRepository _vehicleRepository;
        private readonly VehicleManager _vehicleManager;
        private readonly IRepository<CarModel, Guid> _carModelRepository;
        private readonly IRepository<Fuel, Guid> _fuelRepository;
        private readonly IRepository<Owner, Guid> _ownerRepository;

        public VehiclesAppService(IVehicleRepository vehicleRepository, VehicleManager vehicleManager, IDistributedCache<VehicleExcelDownloadTokenCacheItem, string> excelDownloadTokenCache, IRepository<CarModel, Guid> carModelRepository, IRepository<Fuel, Guid> fuelRepository, IRepository<Owner, Guid> ownerRepository)
        {
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _vehicleRepository = vehicleRepository;
            _vehicleManager = vehicleManager; _carModelRepository = carModelRepository;
            _fuelRepository = fuelRepository;
            _ownerRepository = ownerRepository;
        }

        public virtual async Task<PagedResultDto<VehicleWithNavigationPropertiesDto>> GetListAsync(GetVehiclesInput input)
        {
            var totalCount = await _vehicleRepository.GetCountAsync(input.FilterText, input.Plate, input.CarModelId, input.FuelId, input.OwnerId);
            var items = await _vehicleRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.Plate, input.CarModelId, input.FuelId, input.OwnerId, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<VehicleWithNavigationPropertiesDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<VehicleWithNavigationProperties>, List<VehicleWithNavigationPropertiesDto>>(items)
            };
        }

        public virtual async Task<VehicleWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return ObjectMapper.Map<VehicleWithNavigationProperties, VehicleWithNavigationPropertiesDto>
                (await _vehicleRepository.GetWithNavigationPropertiesAsync(id));
        }

        public virtual async Task<VehicleDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<Vehicle, VehicleDto>(await _vehicleRepository.GetAsync(id));
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetCarModelLookupAsync(LookupRequestDto input)
        {
            var query = (await _carModelRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.Name != null &&
                         x.Name.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<CarModel>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<CarModel>, List<LookupDto<Guid>>>(lookupData)
            };
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetFuelLookupAsync(LookupRequestDto input)
        {
            var query = (await _fuelRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.Name != null &&
                         x.Name.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<Fuel>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Fuel>, List<LookupDto<Guid>>>(lookupData)
            };
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetOwnerLookupAsync(LookupRequestDto input)
        {
            var query = (await _ownerRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.Name != null &&
                         x.Name.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<Owner>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Owner>, List<LookupDto<Guid>>>(lookupData)
            };
        }

        [Authorize(Vehman2Permissions.Vehicles.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _vehicleRepository.DeleteAsync(id);
        }

        [Authorize(Vehman2Permissions.Vehicles.Create)]
        public virtual async Task<VehicleDto> CreateAsync(VehicleCreateDto input)
        {
            if (input.CarModelId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["CarModel"]]);
            }
            if (input.FuelId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["Fuel"]]);
            }
            if (input.OwnerId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["Owner"]]);
            }

            var vehicle = await _vehicleManager.CreateAsync(
            input.CarModelId, input.FuelId, input.OwnerId, input.Plate
            );

            return ObjectMapper.Map<Vehicle, VehicleDto>(vehicle);
        }

        [Authorize(Vehman2Permissions.Vehicles.Edit)]
        public virtual async Task<VehicleDto> UpdateAsync(Guid id, VehicleUpdateDto input)
        {
            if (input.CarModelId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["CarModel"]]);
            }
            if (input.FuelId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["Fuel"]]);
            }
            if (input.OwnerId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["Owner"]]);
            }

            var vehicle = await _vehicleManager.UpdateAsync(
            id,
            input.CarModelId, input.FuelId, input.OwnerId, input.Plate, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<Vehicle, VehicleDto>(vehicle);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(VehicleExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var vehicles = await _vehicleRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.Plate);
            var items = vehicles.Select(item => new
            {
                Plate = item.Vehicle.Plate,

                CarModel = item.CarModel?.Name,
                Fuel = item.Fuel?.Name,
                Owner = item.Owner?.Name,

            });

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(items);
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "Vehicles.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new VehicleExcelDownloadTokenCacheItem { Token = token },
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