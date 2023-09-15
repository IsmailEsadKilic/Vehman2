using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace Vehman2.Fuels
{
    public class FuelManager : DomainService
    {
        private readonly IFuelRepository _fuelRepository;

        public FuelManager(IFuelRepository fuelRepository)
        {
            _fuelRepository = fuelRepository;
        }

        public async Task<Fuel> CreateAsync(
        string name)
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));

            var fuel = new Fuel(
             GuidGenerator.Create(),
             name
             );

            return await _fuelRepository.InsertAsync(fuel);
        }

        public async Task<Fuel> UpdateAsync(
            Guid id,
            string name, [CanBeNull] string concurrencyStamp = null
        )
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));

            var fuel = await _fuelRepository.GetAsync(id);

            fuel.Name = name;

            fuel.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _fuelRepository.UpdateAsync(fuel);
        }

    }
}