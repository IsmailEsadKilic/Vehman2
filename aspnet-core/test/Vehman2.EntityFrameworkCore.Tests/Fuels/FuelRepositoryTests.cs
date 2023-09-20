using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using Vehman2.Fuels;
using Vehman2.EntityFrameworkCore;
using Xunit;

namespace Vehman2.Fuels
{
    public class FuelRepositoryTests : Vehman2EntityFrameworkCoreTestBase
    {
        private readonly IFuelRepository _fuelRepository;

        public FuelRepositoryTests()
        {
            _fuelRepository = GetRequiredService<IFuelRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _fuelRepository.GetListAsync(
                    name: "5975813c04984e0bb22b0a49ebaac8d3cf64f77c41aa4d71accc0ddbb68"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("46f73d6e-832d-47dd-968c-9d461ca65e5f"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _fuelRepository.GetCountAsync(
                    name: "4e2fb80ac8504707b81dbf6a666e66d4be0ded20b40340f59dcf11e1155c12ae"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}