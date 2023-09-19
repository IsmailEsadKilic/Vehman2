using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Vehman2.Owners
{
    public class OwnerCreateDto
    {
        [Required]
        public string Name { get; set; }
        public Guid CompanyId { get; set; }
    }
}