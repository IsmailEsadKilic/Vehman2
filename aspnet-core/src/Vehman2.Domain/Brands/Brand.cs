using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace Vehman2.Brands
{
    public class Brand : FullAuditedAggregateRoot<Guid>
    {
        [NotNull]
        public virtual string Name { get; set; }

        public Brand()
        {

        }

        public Brand(Guid id, string name)
        {

            Id = id;
            Check.NotNull(name, nameof(name));
            Name = name;
        }

    }
}