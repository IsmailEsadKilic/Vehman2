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
            result.Items.Any(x => x.Id == Guid.Parse("4cf1df83-e073-4c7a-bfbc-2284e4efd7e2")).ShouldBe(true);
            result.Items.Any(x => x.Id == Guid.Parse("5fea84d6-e497-4ed0-80a4-299e6418b72b")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _fuelsAppService.GetAsync(Guid.Parse("4cf1df83-e073-4c7a-bfbc-2284e4efd7e2"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("4cf1df83-e073-4c7a-bfbc-2284e4efd7e2"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new FuelCreateDto
            {
                Name = "c2cc0e57a46747d98f3875ef10dfbe175fc67eaa4e8e4c1993387f24e63238825d"
            };

            // Act
            var serviceResult = await _fuelsAppService.CreateAsync(input);

            // Assert
            var result = await _fuelRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Name.ShouldBe("c2cc0e57a46747d98f3875ef10dfbe175fc67eaa4e8e4c1993387f24e63238825d");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new FuelUpdateDto()
            {
                Name = "0b1925364"
            };

            // Act
            var serviceResult = await _fuelsAppService.UpdateAsync(Guid.Parse("4cf1df83-e073-4c7a-bfbc-2284e4efd7e2"), input);

            // Assert
            var result = await _fuelRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Name.ShouldBe("0b1925364");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _fuelsAppService.DeleteAsync(Guid.Parse("4cf1df83-e073-4c7a-bfbc-2284e4efd7e2"));

            // Assert
            var result = await _fuelRepository.FindAsync(c => c.Id == Guid.Parse("4cf1df83-e073-4c7a-bfbc-2284e4efd7e2"));

            result.ShouldBeNull();
        }
    }
}