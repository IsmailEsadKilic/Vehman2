using Vehman2.Shared;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using Vehman2.Shared;

namespace Vehman2.Vehicles
{
    public interface IVehiclesAppService : IApplicationService
    {
        Task<PagedResultDto<VehicleWithNavigationPropertiesDto>> GetListAsync(GetVehiclesInput input);

        Task<VehicleWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id);

        Task<VehicleDto> GetAsync(Guid id);

        Task<PagedResultDto<LookupDto<Guid>>> GetCarModelLookupAsync(LookupRequestDto input);

        Task<PagedResultDto<LookupDto<Guid>>> GetFuelLookupAsync(LookupRequestDto input);

        Task<PagedResultDto<LookupDto<Guid>>> GetOwnerLookupAsync(LookupRequestDto input);

        Task DeleteAsync(Guid id);

        Task<VehicleDto> CreateAsync(VehicleCreateDto input);

        Task<VehicleDto> UpdateAsync(Guid id, VehicleUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(VehicleExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}