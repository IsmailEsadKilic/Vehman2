using Vehman2.Shared;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using Vehman2.Vehicles;
using Volo.Abp.Content;
using Vehman2.Shared;

namespace Vehman2.Controllers.Vehicles
{
    [RemoteService]
    [Area("app")]
    [ControllerName("Vehicle")]
    [Route("api/app/vehicles")]

    public class VehicleController : AbpController, IVehiclesAppService
    {
        private readonly IVehiclesAppService _vehiclesAppService;

        public VehicleController(IVehiclesAppService vehiclesAppService)
        {
            _vehiclesAppService = vehiclesAppService;
        }

        [HttpGet]
        public Task<PagedResultDto<VehicleWithNavigationPropertiesDto>> GetListAsync(GetVehiclesInput input)
        {
            return _vehiclesAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("with-navigation-properties/{id}")]
        public Task<VehicleWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return _vehiclesAppService.GetWithNavigationPropertiesAsync(id);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<VehicleDto> GetAsync(Guid id)
        {
            return _vehiclesAppService.GetAsync(id);
        }

        [HttpGet]
        [Route("car-model-lookup")]
        public Task<PagedResultDto<LookupDto<Guid>>> GetCarModelLookupAsync(LookupRequestDto input)
        {
            return _vehiclesAppService.GetCarModelLookupAsync(input);
        }

        [HttpGet]
        [Route("fuel-lookup")]
        public Task<PagedResultDto<LookupDto<Guid>>> GetFuelLookupAsync(LookupRequestDto input)
        {
            return _vehiclesAppService.GetFuelLookupAsync(input);
        }

        [HttpGet]
        [Route("owner-lookup")]
        public Task<PagedResultDto<LookupDto<Guid>>> GetOwnerLookupAsync(LookupRequestDto input)
        {
            return _vehiclesAppService.GetOwnerLookupAsync(input);
        }

        [HttpPost]
        public virtual Task<VehicleDto> CreateAsync(VehicleCreateDto input)
        {
            return _vehiclesAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<VehicleDto> UpdateAsync(Guid id, VehicleUpdateDto input)
        {
            return _vehiclesAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _vehiclesAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("as-excel-file")]
        public virtual Task<IRemoteStreamContent> GetListAsExcelFileAsync(VehicleExcelDownloadDto input)
        {
            return _vehiclesAppService.GetListAsExcelFileAsync(input);
        }

        [HttpGet]
        [Route("download-token")]
        public Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            return _vehiclesAppService.GetDownloadTokenAsync();
        }
    }
}