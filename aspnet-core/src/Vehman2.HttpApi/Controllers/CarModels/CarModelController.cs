using Vehman2.Shared;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using Vehman2.CarModels;
using Volo.Abp.Content;
using Vehman2.Shared;

namespace Vehman2.Controllers.CarModels
{
    [RemoteService]
    [Area("app")]
    [ControllerName("CarModel")]
    [Route("api/app/car-models")]

    public class CarModelController : AbpController, ICarModelsAppService
    {
        private readonly ICarModelsAppService _carModelsAppService;

        public CarModelController(ICarModelsAppService carModelsAppService)
        {
            _carModelsAppService = carModelsAppService;
        }

        [HttpGet]
        public Task<PagedResultDto<CarModelWithNavigationPropertiesDto>> GetListAsync(GetCarModelsInput input)
        {
            return _carModelsAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("with-navigation-properties/{id}")]
        public Task<CarModelWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return _carModelsAppService.GetWithNavigationPropertiesAsync(id);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<CarModelDto> GetAsync(Guid id)
        {
            return _carModelsAppService.GetAsync(id);
        }

        [HttpGet]
        [Route("brand-lookup")]
        public Task<PagedResultDto<LookupDto<Guid>>> GetBrandLookupAsync(LookupRequestDto input)
        {
            return _carModelsAppService.GetBrandLookupAsync(input);
        }

        [HttpPost]
        public virtual Task<CarModelDto> CreateAsync(CarModelCreateDto input)
        {
            return _carModelsAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<CarModelDto> UpdateAsync(Guid id, CarModelUpdateDto input)
        {
            return _carModelsAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _carModelsAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("as-excel-file")]
        public virtual Task<IRemoteStreamContent> GetListAsExcelFileAsync(CarModelExcelDownloadDto input)
        {
            return _carModelsAppService.GetListAsExcelFileAsync(input);
        }

        [HttpGet]
        [Route("download-token")]
        public Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            return _carModelsAppService.GetDownloadTokenAsync();
        }
    }
}