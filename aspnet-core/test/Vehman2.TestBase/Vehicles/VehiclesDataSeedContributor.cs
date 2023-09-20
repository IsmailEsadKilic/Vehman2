using Vehman2.Owners;
using Vehman2.Fuels;
using Vehman2.CarModels;
using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using Vehman2.Vehicles;

namespace Vehman2.Vehicles
{
    public class VehiclesDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly CarModelsDataSeedContributor _carModelsDataSeedContributor;

        private readonly FuelsDataSeedContributor _fuelsDataSeedContributor;

        private readonly OwnersDataSeedContributor _ownersDataSeedContributor;

        public VehiclesDataSeedContributor(IVehicleRepository vehicleRepository, IUnitOfWorkManager unitOfWorkManager, CarModelsDataSeedContributor carModelsDataSeedContributor, FuelsDataSeedContributor fuelsDataSeedContributor, OwnersDataSeedContributor ownersDataSeedContributor)
        {
            _vehicleRepository = vehicleRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _carModelsDataSeedContributor = carModelsDataSeedContributor; _fuelsDataSeedContributor = fuelsDataSeedContributor; _ownersDataSeedContributor = ownersDataSeedContributor;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _carModelsDataSeedContributor.SeedAsync(context);
            await _fuelsDataSeedContributor.SeedAsync(context);
            await _ownersDataSeedContributor.SeedAsync(context);

            await _vehicleRepository.InsertAsync(new Vehicle
            (
                id: Guid.Parse("96aa94b2-72ce-477b-8d2c-2fbe3f2c0f80"),
                plate: "00dbbc283cbe463eb96e628c99dcccc1fad28c0b98574d7ebe5bafe66a1f814402e74049a3534aeba865d4d378b933b8",
                carModelId: Guid.Parse("dbd9c0c9-b0db-45de-ae1e-9c7e10adb8e0"),
                fuelId: Guid.Parse("4cf1df83-e073-4c7a-bfbc-2284e4efd7e2"),
                ownerId: Guid.Parse("9107552d-e084-473e-ba34-f674f8509e9b")
            ));

            await _vehicleRepository.InsertAsync(new Vehicle
            (
                id: Guid.Parse("56a97d3e-24b6-4a5e-a4bd-4a75a8c32c4f"),
                plate: "cd29f5cdfc2640c28592",
                carModelId: Guid.Parse("dbd9c0c9-b0db-45de-ae1e-9c7e10adb8e0"),
                fuelId: Guid.Parse("4cf1df83-e073-4c7a-bfbc-2284e4efd7e2"),
                ownerId: Guid.Parse("9107552d-e084-473e-ba34-f674f8509e9b")
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}