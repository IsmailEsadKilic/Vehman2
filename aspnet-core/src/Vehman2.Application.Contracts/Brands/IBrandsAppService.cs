using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using Vehman2.Shared;

namespace Vehman2.Brands
{
    public interface IBrandsAppService : IApplicationService
    {
        Task<PagedResultDto<BrandDto>> GetListAsync(GetBrandsInput input);

        Task<BrandDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<BrandDto> CreateAsync(BrandCreateDto input);

        Task<BrandDto> UpdateAsync(Guid id, BrandUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(BrandExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}