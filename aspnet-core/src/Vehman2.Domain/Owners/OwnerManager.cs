using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace Vehman2.Owners
{
    public class OwnerManager : DomainService
    {
        private readonly IOwnerRepository _ownerRepository;

        public OwnerManager(IOwnerRepository ownerRepository)
        {
            _ownerRepository = ownerRepository;
        }

        public async Task<Owner> CreateAsync(
        Guid companyId, string name)
        {
            Check.NotNull(companyId, nameof(companyId));
            Check.NotNullOrWhiteSpace(name, nameof(name));

            var owner = new Owner(
             GuidGenerator.Create(),
             companyId, name
             );

            return await _ownerRepository.InsertAsync(owner);
        }

        public async Task<Owner> UpdateAsync(
            Guid id,
            Guid companyId, string name, [CanBeNull] string concurrencyStamp = null
        )
        {
            Check.NotNull(companyId, nameof(companyId));
            Check.NotNullOrWhiteSpace(name, nameof(name));

            var owner = await _ownerRepository.GetAsync(id);

            owner.CompanyId = companyId;
            owner.Name = name;

            owner.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _ownerRepository.UpdateAsync(owner);
        }

    }
}