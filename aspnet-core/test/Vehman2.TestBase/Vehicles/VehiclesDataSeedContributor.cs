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
                id: Guid.Parse("bdfca121-2bcf-4cdc-9116-a4894c50f597"),
                plate: "a0c41cd",
                carModelId: Guid.Parse("dbd9c0c9-b0db-45de-ae1e-9c7e10adb8e0"),
                fuelId: Guid.Parse("4cf1df83-e073-4c7a-bfbc-2284e4efd7e2"),
                ownerId: Guid.Parse("9107552d-e084-473e-ba34-f674f8509e9b")
            ));

            await _vehicleRepository.InsertAsync(new Vehicle
            (
                id: Guid.Parse("ee1e7303-38b6-440b-8c3a-0a4bbc20b6d4"),
                plate: "15827011a8a04fdbac080b902191c05998cb92d77ec3404989cfc3bf74b4afb1cd342cf3f1684ebf9f6a",
                carModelId: Guid.Parse("dbd9c0c9-b0db-45de-ae1e-9c7e10adb8e0"),
                fuelId: Guid.Parse("4cf1df83-e073-4c7a-bfbc-2284e4efd7e2"),
                ownerId: Guid.Parse("9107552d-e084-473e-ba34-f674f8509e9b")
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}