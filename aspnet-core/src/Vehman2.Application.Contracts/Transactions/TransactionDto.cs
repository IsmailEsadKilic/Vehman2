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
        public string? CompanyName { get; set; }
        public string ConcurrencyStamp { get; set; }
    }

    public class ReportDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public double TotalPrice { get; set; }
        public double TotalLiters { get; set; }
        public Guid VehicleId { get; set; }
        public string? CompanyName { get; set; }

        public virtual double TotalTransactions { get; set; }

        public virtual double AveragePrice { get; set; }

        public virtual double AverageLiters { get; set; }

        public virtual double AveragePricePerLiter { get; set; }

        public virtual double AverageLitersPerTransaction { get; set; }

        public virtual double AveragePricePerTransaction { get; set; }

        public virtual double AverageLitersPerPrice { get; set; }    

        public string ConcurrencyStamp { get; set; }
    }
}