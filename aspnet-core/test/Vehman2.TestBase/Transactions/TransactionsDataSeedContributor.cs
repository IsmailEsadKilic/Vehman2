using Vehman2.Vehicles;
using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using Vehman2.Transactions;

namespace Vehman2.Transactions
{
    public class TransactionsDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly ITransactionRepository _transactionRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly VehiclesDataSeedContributor _vehiclesDataSeedContributor;

        public TransactionsDataSeedContributor(ITransactionRepository transactionRepository, IUnitOfWorkManager unitOfWorkManager, VehiclesDataSeedContributor vehiclesDataSeedContributor)
        {
            _transactionRepository = transactionRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _vehiclesDataSeedContributor = vehiclesDataSeedContributor;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _vehiclesDataSeedContributor.SeedAsync(context);

            await _transactionRepository.InsertAsync(new Transaction
            (
                id: Guid.Parse("d66e9f6f-c594-47ce-9144-7f6c5363cfa2"),
                price: 813024,
                liters: 358831,
                date: new DateTime(2021, 6, 10),
                vehicleId: Guid.Parse("bdfca121-2bcf-4cdc-9116-a4894c50f597")
            ));

            await _transactionRepository.InsertAsync(new Transaction
            (
                id: Guid.Parse("5dc5f042-b71c-4513-ab6d-20cc304462b7"),
                price: 908055,
                liters: 461152,
                date: new DateTime(2021, 2, 12),
                vehicleId: Guid.Parse("bdfca121-2bcf-4cdc-9116-a4894c50f597")
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}