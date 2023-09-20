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
                id: Guid.Parse("7f1e47d3-0d94-45e1-8cf7-794286df8526"),
                name: "cf726c853870419a90e7501f38b0e79aa72aafb0d951477da6e"
            ));

            await _companyRepository.InsertAsync(new Company
            (
                id: Guid.Parse("96407a81-8d2c-42c5-8072-ca35461a2be8"),
                name: "6f42d4b18c6d49fbabc198f9d12a24bf6a6cb006c7"
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}