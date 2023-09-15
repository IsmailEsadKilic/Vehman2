using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using Vehman2.Shared;

namespace Vehman2.Fuels
{
    public interface IFuelsAppService : IApplicationService
    {
        Task<PagedResultDto<FuelDto>> GetListAsync(GetFuelsInput input);

        Task<FuelDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<FuelDto> CreateAsync(FuelCreateDto input);

        Task<FuelDto> UpdateAsync(Guid id, FuelUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(FuelExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}