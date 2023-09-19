using Volo.Abp.Application.Dtos;
using System;

namespace Vehman2.CarModels
{
    public class CarModelExcelDownloadDto
    {
        public string DownloadToken { get; set; }

        public string? FilterText { get; set; }

        public string? Name { get; set; }
        public Guid? BrandId { get; set; }

        public CarModelExcelDownloadDto()
        {

        }
    }
}