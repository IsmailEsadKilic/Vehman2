using Volo.Abp.Application.Dtos;
using System;

namespace Vehman2.Brands
{
    public class GetBrandsInput : PagedAndSortedResultRequestDto
    {
        public string? FilterText { get; set; }

        public string? Name { get; set; }

        public GetBrandsInput()
        {

        }
    }
}