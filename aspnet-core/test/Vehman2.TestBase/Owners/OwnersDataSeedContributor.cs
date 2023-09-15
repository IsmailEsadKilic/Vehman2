using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using Vehman2.Owners;

namespace Vehman2.Owners
{
    public class OwnersDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly IOwnerRepository _ownerRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public OwnersDataSeedContributor(IOwnerRepository ownerRepository, IUnitOfWorkManager unitOfWorkManager)
        {
            _ownerRepository = ownerRepository;
            _unitOfWorkManager = unitOfWorkManager;

        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _ownerRepository.InsertAsync(new Owner
            (
                id: Guid.Parse("a7b90ad5-686d-4c4e-84fb-cb786c5cd180"),
                name: "efba5a42b6a9441c96cfd610b26a3f2afb823c3d615e438a90d8c618dd9a3232d55d2e9c836f43ed903"
            ));

            await _ownerRepository.InsertAsync(new Owner
            (
                id: Guid.Parse("d568ca04-9dac-47ac-81c2-03037efc67d7"),
                name: "be2af4b9a2c34305a8963"
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}