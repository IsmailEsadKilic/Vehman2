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
                    name: "77633a6d115544dbb7bcaec13f7a072e7118"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("250eeac5-f0cf-4aa2-a5ef-600e40478d99"));
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
                    name: "1c5b45bac7f3476ab2c54fdf68f7c0b92aaf895a34b54244a20885fd24189929a0bc4d1f04da40d8ad7ae263"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}