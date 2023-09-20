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
                id: Guid.Parse("42d7b168-8467-4c7b-bcc5-651634ce7a9c"),
                price: 871570,
                liters: 348178,
                date: new DateTime(2016, 3, 19),
                vehicleId: Guid.Parse("bdfca121-2bcf-4cdc-9116-a4894c50f597")
            ));

            await _transactionRepository.InsertAsync(new Transaction
            (
                id: Guid.Parse("34920604-ed6e-4ade-8295-d521fe26b082"),
                price: 25572,
                liters: 658724,
                date: new DateTime(2018, 9, 15),
                vehicleId: Guid.Parse("bdfca121-2bcf-4cdc-9116-a4894c50f597")
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}