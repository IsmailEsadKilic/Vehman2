using Vehman2.Vehicles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Vehman2.EntityFrameworkCore;

namespace Vehman2.Transactions
{
    public class EfCoreTransactionRepository : EfCoreRepository<Vehman2DbContext, Transaction, Guid>, ITransactionRepository
    {
        public EfCoreTransactionRepository(IDbContextProvider<Vehman2DbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<TransactionWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync();

            return (await GetDbSetAsync()).Where(b => b.Id == id)
                .Select(transaction => new TransactionWithNavigationProperties
                {
                    Transaction = transaction,
                    Vehicle = dbContext.Set<Vehicle>().FirstOrDefault(c => c.Id == transaction.VehicleId)
                }).FirstOrDefault();
        }

        public async Task<List<TransactionWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            double? priceMin = null,
            double? priceMax = null,
            double? litersMin = null,
            double? litersMax = null,
            DateTime? dateMin = null,
            DateTime? dateMax = null,
            Guid? vehicleId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, priceMin, priceMax, litersMin, litersMax, dateMin, dateMax, vehicleId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? TransactionConsts.GetDefaultSorting(true) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        protected virtual async Task<IQueryable<TransactionWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
        {
            return from transaction in (await GetDbSetAsync())
                   join vehicle in (await GetDbContextAsync()).Set<Vehicle>() on transaction.VehicleId equals vehicle.Id into vehicles
                   from vehicle in vehicles.DefaultIfEmpty()
                   select new TransactionWithNavigationProperties
                   {
                       Transaction = transaction,
                       Vehicle = vehicle
                   };
        }

        protected virtual IQueryable<TransactionWithNavigationProperties> ApplyFilter(
            IQueryable<TransactionWithNavigationProperties> query,
            string filterText,
            double? priceMin = null,
            double? priceMax = null,
            double? litersMin = null,
            double? litersMax = null,
            DateTime? dateMin = null,
            DateTime? dateMax = null,
            Guid? vehicleId = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => true)
                    .WhereIf(priceMin.HasValue, e => e.Transaction.Price >= priceMin.Value)
                    .WhereIf(priceMax.HasValue, e => e.Transaction.Price <= priceMax.Value)
                    .WhereIf(litersMin.HasValue, e => e.Transaction.Liters >= litersMin.Value)
                    .WhereIf(litersMax.HasValue, e => e.Transaction.Liters <= litersMax.Value)
                    .WhereIf(dateMin.HasValue, e => e.Transaction.Date >= dateMin.Value)
                    .WhereIf(dateMax.HasValue, e => e.Transaction.Date <= dateMax.Value)
                    .WhereIf(vehicleId != null && vehicleId != Guid.Empty, e => e.Vehicle != null && e.Vehicle.Id == vehicleId);
        }

        public async Task<List<Transaction>> GetListAsync(
            string filterText = null,
            double? priceMin = null,
            double? priceMax = null,
            double? litersMin = null,
            double? litersMax = null,
            DateTime? dateMin = null,
            DateTime? dateMax = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, priceMin, priceMax, litersMin, litersMax, dateMin, dateMax);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? TransactionConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            double? priceMin = null,
            double? priceMax = null,
            double? litersMin = null,
            double? litersMax = null,
            DateTime? dateMin = null,
            DateTime? dateMax = null,
            Guid? vehicleId = null,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, priceMin, priceMax, litersMin, litersMax, dateMin, dateMax, vehicleId);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<Transaction> ApplyFilter(
            IQueryable<Transaction> query,
            string filterText,
            double? priceMin = null,
            double? priceMax = null,
            double? litersMin = null,
            double? litersMax = null,
            DateTime? dateMin = null,
            DateTime? dateMax = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => true)
                    .WhereIf(priceMin.HasValue, e => e.Price >= priceMin.Value)
                    .WhereIf(priceMax.HasValue, e => e.Price <= priceMax.Value)
                    .WhereIf(litersMin.HasValue, e => e.Liters >= litersMin.Value)
                    .WhereIf(litersMax.HasValue, e => e.Liters <= litersMax.Value)
                    .WhereIf(dateMin.HasValue, e => e.Date >= dateMin.Value)
                    .WhereIf(dateMax.HasValue, e => e.Date <= dateMax.Value);
        }
    }
}