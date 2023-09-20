using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace Vehman2.Vehicles
{
    public class VehiclesAppServiceTests : Vehman2ApplicationTestBase
    {
        private readonly IVehiclesAppService _vehiclesAppService;
        private readonly IRepository<Vehicle, Guid> _vehicleRepository;

        public VehiclesAppServiceTests()
        {
            _vehiclesAppService = GetRequiredService<IVehiclesAppService>();
            _vehicleRepository = GetRequiredService<IRepository<Vehicle, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _vehiclesAppService.GetListAsync(new GetVehiclesInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.Vehicle.Id == Guid.Parse("96aa94b2-72ce-477b-8d2c-2fbe3f2c0f80")).ShouldBe(true);
            result.Items.Any(x => x.Vehicle.Id == Guid.Parse("56a97d3e-24b6-4a5e-a4bd-4a75a8c32c4f")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _vehiclesAppService.GetAsync(Guid.Parse("96aa94b2-72ce-477b-8d2c-2fbe3f2c0f80"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("96aa94b2-72ce-477b-8d2c-2fbe3f2c0f80"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new VehicleCreateDto
            {
                Plate = "e85ded7733474c",
                CarModelId = Guid.Parse("dbd9c0c9-b0db-45de-ae1e-9c7e10adb8e0"),
                FuelId = Guid.Parse("4cf1df83-e073-4c7a-bfbc-2284e4efd7e2"),
                OwnerId = Guid.Parse("9107552d-e084-473e-ba34-f674f8509e9b")
            };

            // Act
            var serviceResult = await _vehiclesAppService.CreateAsync(input);

            // Assert
            var result = await _vehicleRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Plate.ShouldBe("e85ded7733474c");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new VehicleUpdateDto()
            {
                Plate = "256c700c8eb84ac48519dab06b37264bfa68",
                CarModelId = Guid.Parse("dbd9c0c9-b0db-45de-ae1e-9c7e10adb8e0"),
                FuelId = Guid.Parse("4cf1df83-e073-4c7a-bfbc-2284e4efd7e2"),
                OwnerId = Guid.Parse("9107552d-e084-473e-ba34-f674f8509e9b")
            };

            // Act
            var serviceResult = await _vehiclesAppService.UpdateAsync(Guid.Parse("96aa94b2-72ce-477b-8d2c-2fbe3f2c0f80"), input);

            // Assert
            var result = await _vehicleRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Plate.ShouldBe("256c700c8eb84ac48519dab06b37264bfa68");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _vehiclesAppService.DeleteAsync(Guid.Parse("96aa94b2-72ce-477b-8d2c-2fbe3f2c0f80"));

            // Assert
            var result = await _vehicleRepository.FindAsync(c => c.Id == Guid.Parse("96aa94b2-72ce-477b-8d2c-2fbe3f2c0f80"));

            result.ShouldBeNull();
        }
    }
}