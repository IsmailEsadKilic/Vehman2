using Vehman2.Vehicles;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace Vehman2.Transactions
{
    public class Transaction : FullAuditedAggregateRoot<Guid>
    {
        public virtual double Price { get; set; }

        public virtual double Liters { get; set; }

        public virtual DateTime Date { get; set; }
        public Guid VehicleId { get; set; }

        public Transaction()
        {

        }

        public Transaction(Guid id, Guid vehicleId, double price, double liters, DateTime date)
        {

            Id = id;
            if (price < TransactionConsts.PriceMinLength)
            {
                throw new ArgumentOutOfRangeException(nameof(price), price, "The value of 'price' cannot be lower than " + TransactionConsts.PriceMinLength);
            }

            if (price > TransactionConsts.PriceMaxLength)
            {
                throw new ArgumentOutOfRangeException(nameof(price), price, "The value of 'price' cannot be greater than " + TransactionConsts.PriceMaxLength);
            }

            if (liters < TransactionConsts.LitersMinLength)
            {
                throw new ArgumentOutOfRangeException(nameof(liters), liters, "The value of 'liters' cannot be lower than " + TransactionConsts.LitersMinLength);
            }

            if (liters > TransactionConsts.LitersMaxLength)
            {
                throw new ArgumentOutOfRangeException(nameof(liters), liters, "The value of 'liters' cannot be greater than " + TransactionConsts.LitersMaxLength);
            }

            Price = price;
            Liters = liters;
            Date = date;
            VehicleId = vehicleId;
        }

    }
}