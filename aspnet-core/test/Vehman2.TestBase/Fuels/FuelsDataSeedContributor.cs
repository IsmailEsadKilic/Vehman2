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
                id: Guid.Parse("46f73d6e-832d-47dd-968c-9d461ca65e5f"),
                name: "5975813c04984e0bb22b0a49ebaac8d3cf64f77c41aa4d71accc0ddbb68"
            ));

            await _fuelRepository.InsertAsync(new Fuel
            (
                id: Guid.Parse("6f650f09-c5e3-468d-a7af-75d645a2c4c6"),
                name: "4e2fb80ac8504707b81dbf6a666e66d4be0ded20b40340f59dcf11e1155c12ae"
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}