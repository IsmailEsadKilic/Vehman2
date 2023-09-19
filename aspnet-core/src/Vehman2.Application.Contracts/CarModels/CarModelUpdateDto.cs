using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace Vehman2.CarModels
{
    public class CarModelUpdateDto : IHasConcurrencyStamp
    {
        [Required]
        public string Name { get; set; }
        public Guid BrandId { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}