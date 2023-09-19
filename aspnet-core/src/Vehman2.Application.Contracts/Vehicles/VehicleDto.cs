using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace Vehman2.Vehicles
{
    public class VehicleDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public string Plate { get; set; }
        public Guid CarModelId { get; set; }
        public Guid FuelId { get; set; }
        public Guid OwnerId { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}