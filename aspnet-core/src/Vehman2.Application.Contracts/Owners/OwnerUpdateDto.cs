using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace Vehman2.Owners
{
    public class OwnerUpdateDto : IHasConcurrencyStamp
    {
        [Required]
        public string Name { get; set; }
        public Guid CompanyId { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}