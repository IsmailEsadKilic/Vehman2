using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;
using Volo.Abp.Domain.Entities;

using Volo.Abp;

namespace Vehman2.Fuels
{
    public class Fuel : FullAuditedEntity<Guid>, IHasConcurrencyStamp
    {
        [NotNull]
        public virtual string Name { get; set; }

        public string ConcurrencyStamp { get; set; }

        public Fuel()
        {

        }

        public Fuel(Guid id, string name)
        {
            ConcurrencyStamp = Guid.NewGuid().ToString("N");
            Id = id;
            Check.NotNull(name, nameof(name));
            Name = name;
        }

    }
}