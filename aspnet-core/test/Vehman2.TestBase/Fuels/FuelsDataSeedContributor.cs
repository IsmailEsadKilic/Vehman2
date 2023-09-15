using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using Vehman2.Fuels;

namespace Vehman2.Fuels
{
    public class FuelsDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly IFuelRepository _fuelRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public FuelsDataSeedContributor(IFuelRepository fuelRepository, IUnitOfWorkManager unitOfWorkManager)
        {
            _fuelRepository = fuelRepository;
            _unitOfWorkManager = unitOfWorkManager;

        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _fuelRepository.InsertAsync(new Fuel
            (
                id: Guid.Parse("4cf1df83-e073-4c7a-bfbc-2284e4efd7e2"),
                name: "fab6745bbdee4765aa2b0bc84ce690c"
            ));

            await _fuelRepository.InsertAsync(new Fuel
            (
                id: Guid.Parse("5fea84d6-e497-4ed0-80a4-299e6418b72b"),
                name: "18a87aca9723476f91f3d1c5837ce20ada93bed641d3446892f7ee13d9dc9b9b01fd0a9233344288b082b7a122ea50c58"
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}