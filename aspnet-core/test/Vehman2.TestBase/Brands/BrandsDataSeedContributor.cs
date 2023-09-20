using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using Vehman2.Brands;

namespace Vehman2.Brands
{
    public class BrandsDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly IBrandRepository _brandRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public BrandsDataSeedContributor(IBrandRepository brandRepository, IUnitOfWorkManager unitOfWorkManager)
        {
            _brandRepository = brandRepository;
            _unitOfWorkManager = unitOfWorkManager;

        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _brandRepository.InsertAsync(new Brand
            (
                id: Guid.Parse("3fc80ffa-3435-464d-b377-00b043fa4b6f"),
                name: "4f87a56ff73c485ea7b16af069743fe97347b"
            ));

            await _brandRepository.InsertAsync(new Brand
            (
                id: Guid.Parse("d5186959-9314-4ed4-9a06-4d005a619bbc"),
                name: "ef777194580547ba852c694821c4e7c4aba51efe0fc54b21a9df69f11b23db0981fc2a65275e49089f1613ee6f7bd894"
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}