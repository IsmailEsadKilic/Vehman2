using Volo.Abp.Application.Dtos;
using System;

namespace Vehman2.Fuels
{
    public class FuelExcelDownloadDto
    {
        public string DownloadToken { get; set; }

        public string? FilterText { get; set; }

        public string? Name { get; set; }

        public FuelExcelDownloadDto()
        {

        }
    }
}