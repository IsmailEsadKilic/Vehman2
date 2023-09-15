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
                    name: "fab6745bbdee4765aa2b0bc84ce690c"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("4cf1df83-e073-4c7a-bfbc-2284e4efd7e2"));
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
                    name: "18a87aca9723476f91f3d1c5837ce20ada93bed641d3446892f7ee13d9dc9b9b01fd0a9233344288b082b7a122ea50c58"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}