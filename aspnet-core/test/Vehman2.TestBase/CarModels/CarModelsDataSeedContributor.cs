using Vehman2.Brands;
using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using Vehman2.CarModels;

namespace Vehman2.CarModels
{
    public class CarModelsDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly ICarModelRepository _carModelRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly BrandsDataSeedContributor _brandsDataSeedContributor;

        public CarModelsDataSeedContributor(ICarModelRepository carModelRepository, IUnitOfWorkManager unitOfWorkManager, BrandsDataSeedContributor brandsDataSeedContributor)
        {
            _carModelRepository = carModelRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _brandsDataSeedContributor = brandsDataSeedContributor;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _brandsDataSeedContributor.SeedAsync(context);

            await _carModelRepository.InsertAsync(new CarModel
            (
                id: Guid.Parse("250eeac5-f0cf-4aa2-a5ef-600e40478d99"),
                name: "77633a6d115544dbb7bcaec13f7a072e7118",
                brandId: Guid.Parse("3fc80ffa-3435-464d-b377-00b043fa4b6f")
            ));

            await _carModelRepository.InsertAsync(new CarModel
            (
                id: Guid.Parse("fdad0062-3a74-45a7-96c7-7f9e7312d32f"),
                name: "1c5b45bac7f3476ab2c54fdf68f7c0b92aaf895a34b54244a20885fd24189929a0bc4d1f04da40d8ad7ae263",
                brandId: Guid.Parse("3fc80ffa-3435-464d-b377-00b043fa4b6f")
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}