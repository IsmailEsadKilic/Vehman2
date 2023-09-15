using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Vehman2.CarModels
{
    public class CarModelCreateDto
    {
        [Required]
        public string Name { get; set; }
    }
}