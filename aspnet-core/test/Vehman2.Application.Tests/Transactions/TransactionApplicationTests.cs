using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace Vehman2.Transactions
{
    public class TransactionsAppServiceTests : Vehman2ApplicationTestBase
    {
        private readonly ITransactionsAppService _transactionsAppService;
        private readonly IRepository<Transaction, Guid> _transactionRepository;

        public TransactionsAppServiceTests()
        {
            _transactionsAppService = GetRequiredService<ITransactionsAppService>();
            _transactionRepository = GetRequiredService<IRepository<Transaction, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _transactionsAppService.GetListAsync(new GetTransactionsInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.Transaction.Id == Guid.Parse("42d7b168-8467-4c7b-bcc5-651634ce7a9c")).ShouldBe(true);
            result.Items.Any(x => x.Transaction.Id == Guid.Parse("34920604-ed6e-4ade-8295-d521fe26b082")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _transactionsAppService.GetAsync(Guid.Parse("42d7b168-8467-4c7b-bcc5-651634ce7a9c"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("42d7b168-8467-4c7b-bcc5-651634ce7a9c"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new TransactionCreateDto
            {
                Price = 203573,
                Liters = 107583,
                Date = new DateTime(2008, 8, 18),
                VehicleId = Guid.Parse("bdfca121-2bcf-4cdc-9116-a4894c50f597")
            };

            // Act
            var serviceResult = await _transactionsAppService.CreateAsync(input);

            // Assert
            var result = await _transactionRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Price.ShouldBe(203573);
            result.Liters.ShouldBe(107583);
            result.Date.ShouldBe(new DateTime(2008, 8, 18));
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new TransactionUpdateDto()
            {
                Price = 468360,
                Liters = 785823,
                Date = new DateTime(2016, 1, 25),
                VehicleId = Guid.Parse("bdfca121-2bcf-4cdc-9116-a4894c50f597")
            };

            // Act
            var serviceResult = await _transactionsAppService.UpdateAsync(Guid.Parse("42d7b168-8467-4c7b-bcc5-651634ce7a9c"), input);

            // Assert
            var result = await _transactionRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Price.ShouldBe(468360);
            result.Liters.ShouldBe(785823);
            result.Date.ShouldBe(new DateTime(2016, 1, 25));
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _transactionsAppService.DeleteAsync(Guid.Parse("42d7b168-8467-4c7b-bcc5-651634ce7a9c"));

            // Assert
            var result = await _transactionRepository.FindAsync(c => c.Id == Guid.Parse("42d7b168-8467-4c7b-bcc5-651634ce7a9c"));

            result.ShouldBeNull();
        }
    }
}