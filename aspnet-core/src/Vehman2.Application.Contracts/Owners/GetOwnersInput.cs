using Volo.Abp.Application.Dtos;
using System;

namespace Vehman2.Owners
{
    public class GetOwnersInput : PagedAndSortedResultRequestDto
    {
        public string? FilterText { get; set; }

        public string? Name { get; set; }

        public GetOwnersInput()
        {

        }
    }
}