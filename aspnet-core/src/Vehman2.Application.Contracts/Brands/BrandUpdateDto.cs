using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace Vehman2.Brands
{
    public class BrandUpdateDto : IHasConcurrencyStamp
    {
        [Required]
        public string Name { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}