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
            result.Items.Any(x => x.CarModel.Id == Guid.Parse("250eeac5-f0cf-4aa2-a5ef-600e40478d99")).ShouldBe(true);
            result.Items.Any(x => x.CarModel.Id == Guid.Parse("fdad0062-3a74-45a7-96c7-7f9e7312d32f")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _carModelsAppService.GetAsync(Guid.Parse("250eeac5-f0cf-4aa2-a5ef-600e40478d99"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("250eeac5-f0cf-4aa2-a5ef-600e40478d99"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new CarModelCreateDto
            {
                Name = "45f25397c43847faa880e91961d3ae013aa9a2",
                BrandId = Guid.Parse("3fc80ffa-3435-464d-b377-00b043fa4b6f")
            };

            // Act
            var serviceResult = await _carModelsAppService.CreateAsync(input);

            // Assert
            var result = await _carModelRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Name.ShouldBe("45f25397c43847faa880e91961d3ae013aa9a2");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new CarModelUpdateDto()
            {
                Name = "9ce0d92b5979427b90cff7a88453b2a3877a7f3c257f430cacdbf9309d28",
                BrandId = Guid.Parse("3fc80ffa-3435-464d-b377-00b043fa4b6f")
            };

            // Act
            var serviceResult = await _carModelsAppService.UpdateAsync(Guid.Parse("250eeac5-f0cf-4aa2-a5ef-600e40478d99"), input);

            // Assert
            var result = await _carModelRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Name.ShouldBe("9ce0d92b5979427b90cff7a88453b2a3877a7f3c257f430cacdbf9309d28");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _carModelsAppService.DeleteAsync(Guid.Parse("250eeac5-f0cf-4aa2-a5ef-600e40478d99"));

            // Assert
            var result = await _carModelRepository.FindAsync(c => c.Id == Guid.Parse("250eeac5-f0cf-4aa2-a5ef-600e40478d99"));

            result.ShouldBeNull();
        }
    }
}