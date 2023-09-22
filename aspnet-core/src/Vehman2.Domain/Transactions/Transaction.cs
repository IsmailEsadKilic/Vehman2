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

    public class Report : FullAuditedAggregateRoot<Guid>
    {
        public virtual double TotalPrice { get; set; }

        public virtual double TotalLiters { get; set; }

        public Guid VehicleId { get; set; }

        public string? CompanyName { get; set; }

        public virtual double TotalTransactions { get; set; }

        public virtual double AveragePrice { get; set; }

        public virtual double AverageLiters { get; set; }

        public virtual double AveragePricePerLiter { get; set; }

        public virtual double AverageLitersPerTransaction { get; set; }

        public virtual double AveragePricePerTransaction { get; set; }

        public virtual double AverageLitersPerPrice { get; set; }     

        public Report()
        {

        }

        public Report(Guid id, Guid vehicleId, double totalPrice, double totalLiters, string companyName)
        {

            Id = id;
            if (totalPrice < ReportConsts.TotalPriceMinLength)
            {
                throw new ArgumentOutOfRangeException(nameof(totalPrice), totalPrice, "The value of 'totalPrice' cannot be lower than " + ReportConsts.TotalPriceMinLength);
            }

            if (totalPrice > ReportConsts.TotalPriceMaxLength)
            {
                throw new ArgumentOutOfRangeException(nameof(totalPrice), totalPrice, "The value of 'totalPrice' cannot be greater than " + ReportConsts.TotalPriceMaxLength);
            }

            if (totalLiters < ReportConsts.TotalLitersMinLength)
            {
                throw new ArgumentOutOfRangeException(nameof(totalLiters), totalLiters, "The value of 'totalLiters' cannot be lower than " + ReportConsts.TotalLitersMinLength);
            }

            if (totalLiters > ReportConsts.TotalLitersMaxLength)
            {
                throw new ArgumentOutOfRangeException(nameof(totalLiters), totalLiters, "The value of 'totalLiters' cannot be greater than " + ReportConsts.TotalLitersMaxLength);
            }

            TotalPrice = totalPrice;
            TotalLiters = totalLiters;
            VehicleId = vehicleId;
            CompanyName = companyName;
        }

    }
}