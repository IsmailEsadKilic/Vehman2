using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using Vehman2.Companies;

namespace Vehman2.Companies
{
    public class CompaniesDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly ICompanyRepository _companyRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public CompaniesDataSeedContributor(ICompanyRepository companyRepository, IUnitOfWorkManager unitOfWorkManager)
        {
            _companyRepository = companyRepository;
            _unitOfWorkManager = unitOfWorkManager;

        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _companyRepository.InsertAsync(new Company
            (
                id: Guid.Parse("4c1ffebb-1f01-4052-aabe-d2406b44aaf1"),
                name: "0146d277bb624639b65b8c0719eebdf42640620ebd5f4c63baab5708bec64e0a38cdf47e96f64b70a8510458eba"
            ));

            await _companyRepository.InsertAsync(new Company
            (
                id: Guid.Parse("91d7dff0-1d58-44b2-a06e-2f3d981284c5"),
                name: "61733f4e7b504e8aa608edfc4bde905a5690e265174e48a6b92470944206d52f3"
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}