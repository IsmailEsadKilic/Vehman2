using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace Vehman2.Transactions
{
    public class TransactionUpdateDto : IHasConcurrencyStamp
    {
        [Required]
        [Range(TransactionConsts.PriceMinLength, TransactionConsts.PriceMaxLength)]
        public double Price { get; set; }
        [Range(TransactionConsts.LitersMinLength, TransactionConsts.LitersMaxLength)]
        public double Liters { get; set; }
        public DateTime Date { get; set; }
        public Guid VehicleId { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}