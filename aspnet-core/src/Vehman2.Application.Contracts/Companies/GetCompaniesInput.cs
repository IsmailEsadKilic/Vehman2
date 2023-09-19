using Volo.Abp.Application.Dtos;
using System;

namespace Vehman2.Companies
{
    public class GetCompaniesInput : PagedAndSortedResultRequestDto
    {
        public string? FilterText { get; set; }

        public string? Name { get; set; }

        public GetCompaniesInput()
        {

        }
    }
}