using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace Vehman2.CarModels
{
    public class CarModelManager : DomainService
    {
        private readonly ICarModelRepository _carModelRepository;

        public CarModelManager(ICarModelRepository carModelRepository)
        {
            _carModelRepository = carModelRepository;
        }

        public async Task<CarModel> CreateAsync(
        Guid brandId, string name)
        {
            Check.NotNull(brandId, nameof(brandId));
            Check.NotNullOrWhiteSpace(name, nameof(name));

            var carModel = new CarModel(
             GuidGenerator.Create(),
             brandId, name
             );

            return await _carModelRepository.InsertAsync(carModel);
        }

        public async Task<CarModel> UpdateAsync(
            Guid id,
            Guid brandId, string name, [CanBeNull] string concurrencyStamp = null
        )
        {
            Check.NotNull(brandId, nameof(brandId));
            Check.NotNullOrWhiteSpace(name, nameof(name));

            var carModel = await _carModelRepository.GetAsync(id);

            carModel.BrandId = brandId;
            carModel.Name = name;

            carModel.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _carModelRepository.UpdateAsync(carModel);
        }

    }
}