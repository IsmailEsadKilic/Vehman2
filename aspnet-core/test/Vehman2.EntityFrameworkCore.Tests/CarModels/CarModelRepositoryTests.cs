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
                    name: "81a45372c2044137ac4c69d5df8e31116868ea8c248f4abea3201e98f3"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("dbd9c0c9-b0db-45de-ae1e-9c7e10adb8e0"));
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
                    name: "c21033367ff5430ea2e09d4f473f6c0e4023fc516de4492fb"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}