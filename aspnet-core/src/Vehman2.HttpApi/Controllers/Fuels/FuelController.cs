using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using Vehman2.Fuels;
using Volo.Abp.Content;
using Vehman2.Shared;

namespace Vehman2.Controllers.Fuels
{
    [RemoteService]
    [Area("app")]
    [ControllerName("Fuel")]
    [Route("api/app/fuels")]

    public class FuelController : AbpController, IFuelsAppService
    {
        private readonly IFuelsAppService _fuelsAppService;

        public FuelController(IFuelsAppService fuelsAppService)
        {
            _fuelsAppService = fuelsAppService;
        }

        [HttpGet]
        public virtual Task<PagedResultDto<FuelDto>> GetListAsync(GetFuelsInput input)
        {
            return _fuelsAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<FuelDto> GetAsync(Guid id)
        {
            return _fuelsAppService.GetAsync(id);
        }

        [HttpPost]
        public virtual Task<FuelDto> CreateAsync(FuelCreateDto input)
        {
            return _fuelsAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<FuelDto> UpdateAsync(Guid id, FuelUpdateDto input)
        {
            return _fuelsAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _fuelsAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("as-excel-file")]
        public virtual Task<IRemoteStreamContent> GetListAsExcelFileAsync(FuelExcelDownloadDto input)
        {
            return _fuelsAppService.GetListAsExcelFileAsync(input);
        }

        [HttpGet]
        [Route("download-token")]
        public Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            return _fuelsAppService.GetDownloadTokenAsync();
        }
    }
}