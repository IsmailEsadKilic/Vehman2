using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using Vehman2.CarModels;
using Vehman2.EntityFrameworkCore;
using Xunit;

namespace Vehman2.CarModels
{
    public class CarModelRepositoryTests : Vehman2EntityFrameworkCoreTestBase
    {
        private readonly ICarModelRepository _carModelRepository;

        public CarModelRepositoryTests()
        {
            _carModelRepository = GetRequiredService<ICarModelRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _carModelRepository.GetListAsync(
                    name: "44cc973a"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("324cdde1-e588-43b7-b2b0-070e37d32647"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _carModelRepository.GetCountAsync(
                    name: "df86cc80f63a4c"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}