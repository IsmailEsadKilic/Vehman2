using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Vehman2.Brands
{
    public class BrandCreateDto
    {
        [Required]
        public string Name { get; set; }
    }
}