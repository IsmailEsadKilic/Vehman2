using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace Vehman2.Transactions
{
    public class TransactionManager : DomainService
    {
        private readonly ITransactionRepository _transactionRepository;

        public TransactionManager(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task<Transaction> CreateAsync(
        Guid vehicleId, double price, double liters, DateTime date)
        {
            Check.NotNull(vehicleId, nameof(vehicleId));
            Check.Range(price, nameof(price), TransactionConsts.PriceMinLength, TransactionConsts.PriceMaxLength);
            Check.Range(liters, nameof(liters), TransactionConsts.LitersMinLength, TransactionConsts.LitersMaxLength);
            Check.NotNull(date, nameof(date));

            var transaction = new Transaction(
             GuidGenerator.Create(),
             vehicleId, price, liters, date
             );

            return await _transactionRepository.InsertAsync(transaction);
        }

        public async Task<Transaction> UpdateAsync(
            Guid id,
            Guid vehicleId, double price, double liters, DateTime date, [CanBeNull] string concurrencyStamp = null
        )
        {
            Check.NotNull(vehicleId, nameof(vehicleId));
            Check.Range(price, nameof(price), TransactionConsts.PriceMinLength, TransactionConsts.PriceMaxLength);
            Check.Range(liters, nameof(liters), TransactionConsts.LitersMinLength, TransactionConsts.LitersMaxLength);
            Check.NotNull(date, nameof(date));

            var transaction = await _transactionRepository.GetAsync(id);

            transaction.VehicleId = vehicleId;
            transaction.Price = price;
            transaction.Liters = liters;
            transaction.Date = date;

            transaction.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _transactionRepository.UpdateAsync(transaction);
        }

    }
}