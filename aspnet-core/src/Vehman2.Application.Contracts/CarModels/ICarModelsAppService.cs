using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using Vehman2.Shared;

namespace Vehman2.CarModels
{
    public interface ICarModelsAppService : IApplicationService
    {
        Task<PagedResultDto<CarModelDto>> GetListAsync(GetCarModelsInput input);

        Task<CarModelDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<CarModelDto> CreateAsync(CarModelCreateDto input);

        Task<CarModelDto> UpdateAsync(Guid id, CarModelUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(CarModelExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}