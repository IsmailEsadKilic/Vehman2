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
            result.Items.Any(x => x.CarModel.Id == Guid.Parse("dbd9c0c9-b0db-45de-ae1e-9c7e10adb8e0")).ShouldBe(true);
            result.Items.Any(x => x.CarModel.Id == Guid.Parse("f9ab1ea0-e03a-4b5c-ae83-78026fd85370")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _carModelsAppService.GetAsync(Guid.Parse("dbd9c0c9-b0db-45de-ae1e-9c7e10adb8e0"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("dbd9c0c9-b0db-45de-ae1e-9c7e10adb8e0"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new CarModelCreateDto
            {
                Name = "3f2bef",
                BrandId = Guid.Parse("565f707e-f160-401e-982c-b479e72458db")
            };

            // Act
            var serviceResult = await _carModelsAppService.CreateAsync(input);

            // Assert
            var result = await _carModelRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Name.ShouldBe("3f2bef");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new CarModelUpdateDto()
            {
                Name = "95f96281a43f4e2ba2b64845fa28c581650",
                BrandId = Guid.Parse("565f707e-f160-401e-982c-b479e72458db")
            };

            // Act
            var serviceResult = await _carModelsAppService.UpdateAsync(Guid.Parse("dbd9c0c9-b0db-45de-ae1e-9c7e10adb8e0"), input);

            // Assert
            var result = await _carModelRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Name.ShouldBe("95f96281a43f4e2ba2b64845fa28c581650");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _carModelsAppService.DeleteAsync(Guid.Parse("dbd9c0c9-b0db-45de-ae1e-9c7e10adb8e0"));

            // Assert
            var result = await _carModelRepository.FindAsync(c => c.Id == Guid.Parse("dbd9c0c9-b0db-45de-ae1e-9c7e10adb8e0"));

            result.ShouldBeNull();
        }
    }
}