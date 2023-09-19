using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Vehman2.Companies
{
    public class CompanyCreateDto
    {
        [Required]
        public string Name { get; set; }
    }
}