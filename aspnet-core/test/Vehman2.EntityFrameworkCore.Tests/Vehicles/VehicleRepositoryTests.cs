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
                    plate: "00dbbc283cbe463eb96e628c99dcccc1fad28c0b98574d7ebe5bafe66a1f814402e74049a3534aeba865d4d378b933b8"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("96aa94b2-72ce-477b-8d2c-2fbe3f2c0f80"));
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
                    plate: "cd29f5cdfc2640c28592"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}