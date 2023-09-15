using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using Vehman2.Brands;
using Volo.Abp.Content;
using Vehman2.Shared;

namespace Vehman2.Controllers.Brands
{
    [RemoteService]
    [Area("app")]
    [ControllerName("Brand")]
    [Route("api/app/brands")]

    public class BrandController : AbpController, IBrandsAppService
    {
        private readonly IBrandsAppService _brandsAppService;

        public BrandController(IBrandsAppService brandsAppService)
        {
            _brandsAppService = brandsAppService;
        }

        [HttpGet]
        public virtual Task<PagedResultDto<BrandDto>> GetListAsync(GetBrandsInput input)
        {
            return _brandsAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<BrandDto> GetAsync(Guid id)
        {
            return _brandsAppService.GetAsync(id);
        }

        [HttpPost]
        public virtual Task<BrandDto> CreateAsync(BrandCreateDto input)
        {
            return _brandsAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<BrandDto> UpdateAsync(Guid id, BrandUpdateDto input)
        {
            return _brandsAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _brandsAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("as-excel-file")]
        public virtual Task<IRemoteStreamContent> GetListAsExcelFileAsync(BrandExcelDownloadDto input)
        {
            return _brandsAppService.GetListAsExcelFileAsync(input);
        }

        [HttpGet]
        [Route("download-token")]
        public Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            return _brandsAppService.GetDownloadTokenAsync();
        }
    }
}