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
                id: Guid.Parse("565f707e-f160-401e-982c-b479e72458db"),
                name: "99c9e928970c"
            ));

            await _brandRepository.InsertAsync(new Brand
            (
                id: Guid.Parse("7a2d1d6b-ecce-4403-8223-19256c59cdb4"),
                name: "16b6febf9e544871ac204e226b5f39e45fd39ab78b27476196e0dec92ebfaf168449f7069c3247afbf0d6e0b84164a299f"
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}