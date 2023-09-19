using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using Vehman2.Vehicles;
using Vehman2.EntityFrameworkCore;
using Xunit;

namespace Vehman2.Vehicles
{
    public class VehicleRepositoryTests : Vehman2EntityFrameworkCoreTestBase
    {
        private readonly IVehicleRepository _vehicleRepository;

        public VehicleRepositoryTests()
        {
            _vehicleRepository = GetRequiredService<IVehicleRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _vehicleRepository.GetListAsync(
                    plate: "a0c41cd"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("bdfca121-2bcf-4cdc-9116-a4894c50f597"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _vehicleRepository.GetCountAsync(
                    plate: "15827011a8a04fdbac080b902191c05998cb92d77ec3404989cfc3bf74b4afb1cd342cf3f1684ebf9f6a"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}