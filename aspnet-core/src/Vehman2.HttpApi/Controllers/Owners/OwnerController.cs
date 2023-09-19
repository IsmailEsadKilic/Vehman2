using Vehman2.Shared;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using Vehman2.Owners;
using Volo.Abp.Content;
using Vehman2.Shared;

namespace Vehman2.Controllers.Owners
{
    [RemoteService]
    [Area("app")]
    [ControllerName("Owner")]
    [Route("api/app/owners")]

    public class OwnerController : AbpController, IOwnersAppService
    {
        private readonly IOwnersAppService _ownersAppService;

        public OwnerController(IOwnersAppService ownersAppService)
        {
            _ownersAppService = ownersAppService;
        }

        [HttpGet]
        public Task<PagedResultDto<OwnerWithNavigationPropertiesDto>> GetListAsync(GetOwnersInput input)
        {
            return _ownersAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("with-navigation-properties/{id}")]
        public Task<OwnerWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return _ownersAppService.GetWithNavigationPropertiesAsync(id);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<OwnerDto> GetAsync(Guid id)
        {
            return _ownersAppService.GetAsync(id);
        }

        [HttpGet]
        [Route("company-lookup")]
        public Task<PagedResultDto<LookupDto<Guid>>> GetCompanyLookupAsync(LookupRequestDto input)
        {
            return _ownersAppService.GetCompanyLookupAsync(input);
        }

        [HttpPost]
        public virtual Task<OwnerDto> CreateAsync(OwnerCreateDto input)
        {
            return _ownersAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<OwnerDto> UpdateAsync(Guid id, OwnerUpdateDto input)
        {
            return _ownersAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _ownersAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("as-excel-file")]
        public virtual Task<IRemoteStreamContent> GetListAsExcelFileAsync(OwnerExcelDownloadDto input)
        {
            return _ownersAppService.GetListAsExcelFileAsync(input);
        }

        [HttpGet]
        [Route("download-token")]
        public Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            return _ownersAppService.GetDownloadTokenAsync();
        }
    }
}