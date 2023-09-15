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
        Task<PagedResultDto<OwnerDto>> GetListAsync(GetOwnersInput input);

        Task<OwnerDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<OwnerDto> CreateAsync(OwnerCreateDto input);

        Task<OwnerDto> UpdateAsync(Guid id, OwnerUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(OwnerExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}