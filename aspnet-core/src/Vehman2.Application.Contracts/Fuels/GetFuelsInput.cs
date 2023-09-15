using Volo.Abp.Application.Dtos;
using System;

namespace Vehman2.Fuels
{
    public class GetFuelsInput : PagedAndSortedResultRequestDto
    {
        public string? FilterText { get; set; }

        public string? Name { get; set; }

        public GetFuelsInput()
        {

        }
    }
}