using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Vehman2.Fuels
{
    public class FuelCreateDto
    {
        [Required]
        public string Name { get; set; }
    }
}