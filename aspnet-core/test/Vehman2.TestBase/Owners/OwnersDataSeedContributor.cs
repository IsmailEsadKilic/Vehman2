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
                id: Guid.Parse("3d73a488-c5cb-4921-b477-00248e97f36a"),
                name: "7deff88ab28f4bcc94e48ae36b49bf4",
                companyId: Guid.Parse("7f1e47d3-0d94-45e1-8cf7-794286df8526")
            ));

            await _ownerRepository.InsertAsync(new Owner
            (
                id: Guid.Parse("1016182a-5752-4509-9c5f-47c4afece02b"),
                name: "6be0de4714c347169af0e18259e4106cb9302ffcb07c46528d8f060827faea398595817be0194ae3afd9bb9a96555735c1",
                companyId: Guid.Parse("7f1e47d3-0d94-45e1-8cf7-794286df8526")
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}