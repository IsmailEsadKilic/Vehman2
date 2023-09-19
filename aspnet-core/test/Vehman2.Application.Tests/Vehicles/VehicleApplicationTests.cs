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
            result.Items.Any(x => x.Vehicle.Id == Guid.Parse("bdfca121-2bcf-4cdc-9116-a4894c50f597")).ShouldBe(true);
            result.Items.Any(x => x.Vehicle.Id == Guid.Parse("ee1e7303-38b6-440b-8c3a-0a4bbc20b6d4")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _vehiclesAppService.GetAsync(Guid.Parse("bdfca121-2bcf-4cdc-9116-a4894c50f597"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("bdfca121-2bcf-4cdc-9116-a4894c50f597"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new VehicleCreateDto
            {
                Plate = "f8e3736b7dd34dec84c404846f6e3ecd9f3f58fbb611442499aaba5",
                CarModelId = Guid.Parse("dbd9c0c9-b0db-45de-ae1e-9c7e10adb8e0"),
                FuelId = Guid.Parse("4cf1df83-e073-4c7a-bfbc-2284e4efd7e2"),
                OwnerId = Guid.Parse("9107552d-e084-473e-ba34-f674f8509e9b")
            };

            // Act
            var serviceResult = await _vehiclesAppService.CreateAsync(input);

            // Assert
            var result = await _vehicleRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Plate.ShouldBe("f8e3736b7dd34dec84c404846f6e3ecd9f3f58fbb611442499aaba5");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new VehicleUpdateDto()
            {
                Plate = "c3368989a53d472294a2d5728967716ecee315bc56474f4da18197055622d3e88edadacda6784a5eb7b1483c7b0eeaf",
                CarModelId = Guid.Parse("dbd9c0c9-b0db-45de-ae1e-9c7e10adb8e0"),
                FuelId = Guid.Parse("4cf1df83-e073-4c7a-bfbc-2284e4efd7e2"),
                OwnerId = Guid.Parse("9107552d-e084-473e-ba34-f674f8509e9b")
            };

            // Act
            var serviceResult = await _vehiclesAppService.UpdateAsync(Guid.Parse("bdfca121-2bcf-4cdc-9116-a4894c50f597"), input);

            // Assert
            var result = await _vehicleRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Plate.ShouldBe("c3368989a53d472294a2d5728967716ecee315bc56474f4da18197055622d3e88edadacda6784a5eb7b1483c7b0eeaf");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _vehiclesAppService.DeleteAsync(Guid.Parse("bdfca121-2bcf-4cdc-9116-a4894c50f597"));

            // Assert
            var result = await _vehicleRepository.FindAsync(c => c.Id == Guid.Parse("bdfca121-2bcf-4cdc-9116-a4894c50f597"));

            result.ShouldBeNull();
        }
    }
}