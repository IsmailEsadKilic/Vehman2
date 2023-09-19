using Volo.Abp.Application.Dtos;
using System;

namespace Vehman2.CarModels
{
    public class GetCarModelsInput : PagedAndSortedResultRequestDto
    {
        public string? FilterText { get; set; }

        public string? Name { get; set; }
        public Guid? BrandId { get; set; }

        public GetCarModelsInput()
        {

        }
    }
}