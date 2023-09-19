using Volo.Abp.Application.Dtos;
using System;

namespace Vehman2.Companies
{
    public class CompanyExcelDownloadDto
    {
        public string DownloadToken { get; set; }

        public string? FilterText { get; set; }

        public string? Name { get; set; }

        public CompanyExcelDownloadDto()
        {

        }
    }
}