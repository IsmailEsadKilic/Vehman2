using Vehman2.CarModels;
using Vehman2.Fuels;
using Vehman2.Owners;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace Vehman2.Vehicles
{
    public class Vehicle : FullAuditedAggregateRoot<Guid>
    {
        [NotNull]
        public virtual string Plate { get; set; }
        public Guid CarModelId { get; set; }
        public Guid FuelId { get; set; }
        public Guid OwnerId { get; set; }

        public Vehicle()
        {

        }

        public Vehicle(Guid id, Guid carModelId, Guid fuelId, Guid ownerId, string plate)
        {

            Id = id;
            Check.NotNull(plate, nameof(plate));
            Plate = plate;
            CarModelId = carModelId;
            FuelId = fuelId;
            OwnerId = ownerId;
        }

    }
}