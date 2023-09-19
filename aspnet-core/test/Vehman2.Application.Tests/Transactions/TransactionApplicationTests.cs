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
            result.Items.Any(x => x.Transaction.Id == Guid.Parse("d66e9f6f-c594-47ce-9144-7f6c5363cfa2")).ShouldBe(true);
            result.Items.Any(x => x.Transaction.Id == Guid.Parse("5dc5f042-b71c-4513-ab6d-20cc304462b7")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _transactionsAppService.GetAsync(Guid.Parse("d66e9f6f-c594-47ce-9144-7f6c5363cfa2"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("d66e9f6f-c594-47ce-9144-7f6c5363cfa2"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new TransactionCreateDto
            {
                Price = 388997,
                Liters = 708071,
                Date = new DateTime(2013, 2, 8),
                VehicleId = Guid.Parse("bdfca121-2bcf-4cdc-9116-a4894c50f597")
            };

            // Act
            var serviceResult = await _transactionsAppService.CreateAsync(input);

            // Assert
            var result = await _transactionRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Price.ShouldBe(388997);
            result.Liters.ShouldBe(708071);
            result.Date.ShouldBe(new DateTime(2013, 2, 8));
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new TransactionUpdateDto()
            {
                Price = 394658,
                Liters = 34063,
                Date = new DateTime(2003, 5, 27),
                VehicleId = Guid.Parse("bdfca121-2bcf-4cdc-9116-a4894c50f597")
            };

            // Act
            var serviceResult = await _transactionsAppService.UpdateAsync(Guid.Parse("d66e9f6f-c594-47ce-9144-7f6c5363cfa2"), input);

            // Assert
            var result = await _transactionRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Price.ShouldBe(394658);
            result.Liters.ShouldBe(34063);
            result.Date.ShouldBe(new DateTime(2003, 5, 27));
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _transactionsAppService.DeleteAsync(Guid.Parse("d66e9f6f-c594-47ce-9144-7f6c5363cfa2"));

            // Assert
            var result = await _transactionRepository.FindAsync(c => c.Id == Guid.Parse("d66e9f6f-c594-47ce-9144-7f6c5363cfa2"));

            result.ShouldBeNull();
        }
    }
}