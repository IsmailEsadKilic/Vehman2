using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace Vehman2.CarModels
{
    public class CarModelDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public string Name { get; set; }
        public Guid BrandId { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}