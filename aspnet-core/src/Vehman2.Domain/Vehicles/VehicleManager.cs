using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace Vehman2.Vehicles
{
    public class VehicleManager : DomainService
    {
        private readonly IVehicleRepository _vehicleRepository;

        public VehicleManager(IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        public async Task<Vehicle> CreateAsync(
        Guid carModelId, Guid fuelId, Guid ownerId, string plate)
        {
            Check.NotNull(carModelId, nameof(carModelId));
            Check.NotNull(fuelId, nameof(fuelId));
            Check.NotNull(ownerId, nameof(ownerId));
            Check.NotNullOrWhiteSpace(plate, nameof(plate));

            var vehicle = new Vehicle(
             GuidGenerator.Create(),
             carModelId, fuelId, ownerId, plate
             );

            return await _vehicleRepository.InsertAsync(vehicle);
        }

        public async Task<Vehicle> UpdateAsync(
            Guid id,
            Guid carModelId, Guid fuelId, Guid ownerId, string plate, [CanBeNull] string concurrencyStamp = null
        )
        {
            Check.NotNull(carModelId, nameof(carModelId));
            Check.NotNull(fuelId, nameof(fuelId));
            Check.NotNull(ownerId, nameof(ownerId));
            Check.NotNullOrWhiteSpace(plate, nameof(plate));

            var vehicle = await _vehicleRepository.GetAsync(id);

            vehicle.CarModelId = carModelId;
            vehicle.FuelId = fuelId;
            vehicle.OwnerId = ownerId;
            vehicle.Plate = plate;

            vehicle.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _vehicleRepository.UpdateAsync(vehicle);
        }

    }
}