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

        public CarModelsDataSeedContributor(ICarModelRepository carModelRepository, IUnitOfWorkManager unitOfWorkManager)
        {
            _carModelRepository = carModelRepository;
            _unitOfWorkManager = unitOfWorkManager;

        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _carModelRepository.InsertAsync(new CarModel
            (
                id: Guid.Parse("324cdde1-e588-43b7-b2b0-070e37d32647"),
                name: "44cc973a"
            ));

            await _carModelRepository.InsertAsync(new CarModel
            (
                id: Guid.Parse("de6b2e52-03ac-4922-8452-621beae15bcd"),
                name: "df86cc80f63a4c"
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}