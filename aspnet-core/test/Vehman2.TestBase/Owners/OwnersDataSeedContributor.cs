using Vehman2.Companies;
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
        private readonly CompaniesDataSeedContributor _companiesDataSeedContributor;

        public OwnersDataSeedContributor(IOwnerRepository ownerRepository, IUnitOfWorkManager unitOfWorkManager, CompaniesDataSeedContributor companiesDataSeedContributor)
        {
            _ownerRepository = ownerRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _companiesDataSeedContributor = companiesDataSeedContributor;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _companiesDataSeedContributor.SeedAsync(context);

            await _ownerRepository.InsertAsync(new Owner
            (
                id: Guid.Parse("9107552d-e084-473e-ba34-f674f8509e9b"),
                name: "28db860b95634be8b0d6cb99327908e3c9c0585b04844469aa70d1c847b872283c928e20603447629",
                companyId: Guid.Parse("4c1ffebb-1f01-4052-aabe-d2406b44aaf1")
            ));

            await _ownerRepository.InsertAsync(new Owner
            (
                id: Guid.Parse("a48e770b-d759-4487-ab3f-29f6002c9775"),
                name: "fe5004b7",
                companyId: Guid.Parse("4c1ffebb-1f01-4052-aabe-d2406b44aaf1")
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}