using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace Vehman2.Vehicles
{
    public class VehicleUpdateDto : IHasConcurrencyStamp
    {
        [Required]
        public string Plate { get; set; }
        public Guid CarModelId { get; set; }
        public Guid FuelId { get; set; }
        public Guid OwnerId { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}