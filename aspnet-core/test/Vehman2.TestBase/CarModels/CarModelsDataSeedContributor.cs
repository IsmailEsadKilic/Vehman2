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
                id: Guid.Parse("dbd9c0c9-b0db-45de-ae1e-9c7e10adb8e0"),
                name: "81a45372c2044137ac4c69d5df8e31116868ea8c248f4abea3201e98f3",
                brandId: Guid.Parse("565f707e-f160-401e-982c-b479e72458db")
            ));

            await _carModelRepository.InsertAsync(new CarModel
            (
                id: Guid.Parse("f9ab1ea0-e03a-4b5c-ae83-78026fd85370"),
                name: "c21033367ff5430ea2e09d4f473f6c0e4023fc516de4492fb",
                brandId: Guid.Parse("565f707e-f160-401e-982c-b479e72458db")
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}