using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace Vehman2.Owners
{
    public class Owner : FullAuditedAggregateRoot<Guid>
    {
        [NotNull]
        public virtual string Name { get; set; }

        public Owner()
        {

        }

        public Owner(Guid id, string name)
        {

            Id = id;
            Check.NotNull(name, nameof(name));
            Name = name;
        }

    }
}