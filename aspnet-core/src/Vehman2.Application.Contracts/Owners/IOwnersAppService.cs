using Vehman2.Shared;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using Vehman2.Shared;

namespace Vehman2.Owners
{
    public interface IOwnersAppService : IApplicationService
    {
        Task<PagedResultDto<OwnerWithNavigationPropertiesDto>> GetListAsync(GetOwnersInput input);

        Task<OwnerWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id);

        Task<OwnerDto> GetAsync(Guid id);

        Task<PagedResultDto<LookupDto<Guid>>> GetCompanyLookupAsync(LookupRequestDto input);

        Task DeleteAsync(Guid id);

        Task<OwnerDto> CreateAsync(OwnerCreateDto input);

        Task<OwnerDto> UpdateAsync(Guid id, OwnerUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(OwnerExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}