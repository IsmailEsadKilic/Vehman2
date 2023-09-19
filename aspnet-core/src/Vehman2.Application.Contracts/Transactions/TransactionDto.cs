using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace Vehman2.Transactions
{
    public class TransactionDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public double Price { get; set; }
        public double Liters { get; set; }
        public DateTime Date { get; set; }
        public Guid VehicleId { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}