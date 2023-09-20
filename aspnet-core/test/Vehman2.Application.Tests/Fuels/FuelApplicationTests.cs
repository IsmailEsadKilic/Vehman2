using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace Vehman2.Fuels
{
    public class FuelsAppServiceTests : Vehman2ApplicationTestBase
    {
        private readonly IFuelsAppService _fuelsAppService;
        private readonly IRepository<Fuel, Guid> _fuelRepository;

        public FuelsAppServiceTests()
        {
            _fuelsAppService = GetRequiredService<IFuelsAppService>();
            _fuelRepository = GetRequiredService<IRepository<Fuel, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _fuelsAppService.GetListAsync(new GetFuelsInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.Id == Guid.Parse("46f73d6e-832d-47dd-968c-9d461ca65e5f")).ShouldBe(true);
            result.Items.Any(x => x.Id == Guid.Parse("6f650f09-c5e3-468d-a7af-75d645a2c4c6")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _fuelsAppService.GetAsync(Guid.Parse("46f73d6e-832d-47dd-968c-9d461ca65e5f"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("46f73d6e-832d-47dd-968c-9d461ca65e5f"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new FuelCreateDto
            {
                Name = "245802f6d"
            };

            // Act
            var serviceResult = await _fuelsAppService.CreateAsync(input);

            // Assert
            var result = await _fuelRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Name.ShouldBe("245802f6d");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new FuelUpdateDto()
            {
                Name = "d0d5c2d2e9a54016ad7486b862b5c71e3c7dc815755c4a1e"
            };

            // Act
            var serviceResult = await _fuelsAppService.UpdateAsync(Guid.Parse("46f73d6e-832d-47dd-968c-9d461ca65e5f"), input);

            // Assert
            var result = await _fuelRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Name.ShouldBe("d0d5c2d2e9a54016ad7486b862b5c71e3c7dc815755c4a1e");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _fuelsAppService.DeleteAsync(Guid.Parse("46f73d6e-832d-47dd-968c-9d461ca65e5f"));

            // Assert
            var result = await _fuelRepository.FindAsync(c => c.Id == Guid.Parse("46f73d6e-832d-47dd-968c-9d461ca65e5f"));

            result.ShouldBeNull();
        }
    }
}