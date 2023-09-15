using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace Vehman2.Brands
{
    public class BrandManager : DomainService
    {
        private readonly IBrandRepository _brandRepository;

        public BrandManager(IBrandRepository brandRepository)
        {
            _brandRepository = brandRepository;
        }

        public async Task<Brand> CreateAsync(
        string name)
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));

            var brand = new Brand(
             GuidGenerator.Create(),
             name
             );

            return await _brandRepository.InsertAsync(brand);
        }

        public async Task<Brand> UpdateAsync(
            Guid id,
            string name, [CanBeNull] string concurrencyStamp = null
        )
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));

            var brand = await _brandRepository.GetAsync(id);

            brand.Name = name;

            brand.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _brandRepository.UpdateAsync(brand);
        }

    }
}