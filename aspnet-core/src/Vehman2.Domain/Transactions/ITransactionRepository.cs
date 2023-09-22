using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Vehman2.Transactions
{
    public interface ITransactionRepository : IRepository<Transaction, Guid>
    {
        Task<TransactionWithNavigationProperties> GetWithNavigationPropertiesAsync(
            Guid id,
            CancellationToken cancellationToken = default
        );

        Task<List<TransactionWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
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
            CancellationToken cancellationToken = default
        );

        Task<List<ReportWithNavigationProperties>> GetReportListWithNavigationPropertiesAsync(
            string filterText = null,
            DateTime? dateMin = null,
            DateTime? dateMax = null,
            Guid? vehicleId = null,
            string CompanyName = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<List<Transaction>> GetListAsync(
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
                    CancellationToken cancellationToken = default
                );

        Task<long> GetCountAsync(
            string filterText = null,
            double? priceMin = null,
            double? priceMax = null,
            double? litersMin = null,
            double? litersMax = null,
            DateTime? dateMin = null,
            DateTime? dateMax = null,
            Guid? vehicleId = null,
            string? CompanyName = null,
            CancellationToken cancellationToken = default
        );

        Task<long> GetReportCountAsync(
            string filterText = null,
            DateTime? dateMin = null,
            DateTime? dateMax = null,
            Guid? vehicleId = null,
            string CompanyName = null,
            CancellationToken cancellationToken = default
        );
    }
}