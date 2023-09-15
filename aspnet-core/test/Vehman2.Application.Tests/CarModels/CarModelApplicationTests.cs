using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace Vehman2.CarModels
{
    public class CarModelsAppServiceTests : Vehman2ApplicationTestBase
    {
        private readonly ICarModelsAppService _carModelsAppService;
        private readonly IRepository<CarModel, Guid> _carModelRepository;

        public CarModelsAppServiceTests()
        {
            _carModelsAppService = GetRequiredService<ICarModelsAppService>();
            _carModelRepository = GetRequiredService<IRepository<CarModel, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _carModelsAppService.GetListAsync(new GetCarModelsInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.Id == Guid.Parse("324cdde1-e588-43b7-b2b0-070e37d32647")).ShouldBe(true);
            result.Items.Any(x => x.Id == Guid.Parse("de6b2e52-03ac-4922-8452-621beae15bcd")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _carModelsAppService.GetAsync(Guid.Parse("324cdde1-e588-43b7-b2b0-070e37d32647"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("324cdde1-e588-43b7-b2b0-070e37d32647"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new CarModelCreateDto
            {
                Name = "e2bd4fb523f04802ba"
            };

            // Act
            var serviceResult = await _carModelsAppService.CreateAsync(input);

            // Assert
            var result = await _carModelRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Name.ShouldBe("e2bd4fb523f04802ba");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new CarModelUpdateDto()
            {
                Name = "37f19c78ed0640348e7002a848938a9a5680a8"
            };

            // Act
            var serviceResult = await _carModelsAppService.UpdateAsync(Guid.Parse("324cdde1-e588-43b7-b2b0-070e37d32647"), input);

            // Assert
            var result = await _carModelRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Name.ShouldBe("37f19c78ed0640348e7002a848938a9a5680a8");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _carModelsAppService.DeleteAsync(Guid.Parse("324cdde1-e588-43b7-b2b0-070e37d32647"));

            // Assert
            var result = await _carModelRepository.FindAsync(c => c.Id == Guid.Parse("324cdde1-e588-43b7-b2b0-070e37d32647"));

            result.ShouldBeNull();
        }
    }
}