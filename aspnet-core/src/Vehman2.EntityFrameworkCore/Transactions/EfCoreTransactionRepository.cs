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
using Vehman2.Companies;
using Vehman2.Owners;
using Microsoft.AspNetCore.Authorization;
using System.Linq.Expressions;

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
            string companyName = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {   
            try
            {
                if (sorting == "companyName"|| sorting.Contains("companyName"))
                {
                    sorting = null;
                }
            }
            catch
            {

            }

            Console.WriteLine("++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
            Console.WriteLine("EfCoreTransactionRepository.GetListWithNavigationPropertiesAsync");
            Console.WriteLine($"sorting: {sorting}");
            Console.WriteLine($"companyName: {companyName}");
            var query = await GetQueryForNavigationPropertiesAsync();
            var allowedOwnerIds = await GetOwnersForCompany(companyName); //list of Guids
            query = ApplyFilter(query, filterText, priceMin, priceMax, litersMin, litersMax, dateMin, dateMax, vehicleId, allowedOwnerIds);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? TransactionConsts.GetDefaultSorting(true) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<List<ReportWithNavigationProperties>> GetReportListWithNavigationPropertiesAsync(
            string filterText = null,
            DateTime? dateMin = null,
            DateTime? dateMax = null,
            Guid? vehicleId = null,
            string companyName = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            try
            {
                if (sorting == "plate"|| sorting.Contains("plate"))
                {

                }
                else
                {
                   sorting = null;
                }
            }
            catch
            {

            }
            
            Console.WriteLine("ffffffffffffffffffffffffffffffffffffffffffffffffffff filterText: " + filterText);
            var query = await GetQueryForNavigationPropertiesAsync();
            var allowedOwnerIds = await GetOwnersForCompany(companyName); //list of Guids
            query = ApplyFilter(query, filterText, 0, 999999, 0, 999999, dateMin, dateMax, vehicleId, allowedOwnerIds);
            query = query.OrderBy(string.IsNullOrWhiteSpace(null) ? TransactionConsts.GetDefaultSorting(true) : sorting);

            var transactions = await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);

            var reportList = new List<ReportWithNavigationProperties>();

            var VehToTr = new Dictionary<Guid, List<TransactionWithNavigationProperties>>();
            for (int i = 0; i < transactions.Count; i++)
            {
                var transaction = transactions[i];
                if (VehToTr.ContainsKey(transaction.Vehicle.Id))
                {
                    VehToTr[transaction.Vehicle.Id].Add(transaction);
                }
                else
                {
                    VehToTr.Add(transaction.Vehicle.Id, new List<TransactionWithNavigationProperties> { transaction });
                }
            }

            for (int i = 0; i < VehToTr.Count; i++)
            {
                var trs = VehToTr.ElementAt(i).Value;
                var report = new ReportWithNavigationProperties
                {
                    Vehicle = trs[0].Vehicle,
                    Report = new Report
                    {
                        TotalLiters = trs.Sum(t => t.Transaction.Liters),
                        TotalPrice = trs.Sum(t => t.Transaction.Price),
                        TotalTransactions = trs.Count,
                        AveragePrice = trs.Average(t => t.Transaction.Price),
                        AverageLiters = trs.Average(t => t.Transaction.Liters),
                        AveragePricePerLiter = trs.Average(t => t.Transaction.Price / t.Transaction.Liters),
                        AverageLitersPerTransaction = trs.Average(t => t.Transaction.Liters / trs.Count),
                        AveragePricePerTransaction = trs.Average(t => t.Transaction.Price / trs.Count),
                        AverageLitersPerPrice = trs.Average(t => t.Transaction.Liters / t.Transaction.Price)
                    }
                };

                reportList.Add(report);
            }

            return reportList;

            //000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000
            throw new NotImplementedException();
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
            Guid? vehicleId = null,
            List<Guid>? allowedOwnerIds = null
            )
        {   
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => true)
                    .WhereIf(priceMin.HasValue, e => e.Transaction.Price >= priceMin.Value)
                    .WhereIf(priceMax.HasValue, e => e.Transaction.Price <= priceMax.Value)
                    .WhereIf(litersMin.HasValue, e => e.Transaction.Liters >= litersMin.Value)
                    .WhereIf(litersMax.HasValue, e => e.Transaction.Liters <= litersMax.Value)
                    .WhereIf(dateMin.HasValue, e => e.Transaction.Date >= dateMin.Value)
                    .WhereIf(dateMax.HasValue, e => e.Transaction.Date <= dateMax.Value)
                    .WhereIf(vehicleId != null && vehicleId != Guid.Empty, e => e.Vehicle != null && e.Vehicle.Id == vehicleId)
                    .WhereIf(allowedOwnerIds != null && allowedOwnerIds.Count > 0, e => e.Vehicle != null && allowedOwnerIds.Contains(e.Vehicle.OwnerId));
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
            string? companyName = null,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            var allowedOwnerIds = await GetOwnersForCompany(companyName); //list of Guids
            query = ApplyFilter(query, filterText, priceMin, priceMax, litersMin, litersMax, dateMin, dateMax, vehicleId, allowedOwnerIds);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        public async Task<long> GetReportCountAsync(
            string filterText = null,
            DateTime? dateMin = null,
            DateTime? dateMax = null,
            Guid? vehicleId = null,
            string CompanyName = null,
            CancellationToken cancellationToken = default)

        {
            var list = await GetReportListWithNavigationPropertiesAsync(filterText, dateMin, dateMax, vehicleId, CompanyName, null, int.MaxValue, 0, cancellationToken);
            return list.Count;

            //000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000
            throw new NotImplementedException();
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

        public async Task<List<Guid>> GetOwnersForCompany(string companyName)
        {
            //convert companyName to guid

            try
            {
                var guid = Guid.Parse(companyName);
                var dbContext = await GetDbContextAsync();
                var company = dbContext.Set<Company>().FirstOrDefault(c => c.Id == guid);
                if (company == null)
                {
                    return null;
                }
                else
                {
                    return dbContext.Set<Owner>().Where(o => o.CompanyId == company.Id).Select(o => o.Id).ToList();
                }
            }
            catch (Exception)
            {
                Console.WriteLine("EfCoreTransactionRepository.GetOwnersForCompany: Exception--------------------------------------------------");
                //return all owners
                try
                {
                    var dbContext = await GetDbContextAsync();
                    return dbContext.Set<Owner>().Select(o => o.Id).ToList();
                }
                catch (Exception)
                {
                    Console.WriteLine("EfCoreTransactionRepository.GetOwnersForCompany: Exception--------------------------------------------------");
                    return null;
                }
            }

        }
    }
}