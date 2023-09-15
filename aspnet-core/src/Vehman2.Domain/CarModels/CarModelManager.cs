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
        string name)
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));

            var carModel = new CarModel(
             GuidGenerator.Create(),
             name
             );

            return await _carModelRepository.InsertAsync(carModel);
        }

        public async Task<CarModel> UpdateAsync(
            Guid id,
            string name, [CanBeNull] string concurrencyStamp = null
        )
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));

            var carModel = await _carModelRepository.GetAsync(id);

            carModel.Name = name;

            carModel.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _carModelRepository.UpdateAsync(carModel);
        }

    }
}