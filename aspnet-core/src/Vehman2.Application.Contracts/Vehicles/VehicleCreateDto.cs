using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Vehman2.Vehicles
{
    public class VehicleCreateDto
    {
        [Required]
        public string Plate { get; set; }
        public Guid CarModelId { get; set; }
        public Guid FuelId { get; set; }
        public Guid OwnerId { get; set; }
    }
}