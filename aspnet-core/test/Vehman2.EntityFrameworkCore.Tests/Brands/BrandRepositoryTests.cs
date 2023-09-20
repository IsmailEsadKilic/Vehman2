using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using Vehman2.Brands;
using Vehman2.EntityFrameworkCore;
using Xunit;

namespace Vehman2.Brands
{
    public class BrandRepositoryTests : Vehman2EntityFrameworkCoreTestBase
    {
        private readonly IBrandRepository _brandRepository;

        public BrandRepositoryTests()
        {
            _brandRepository = GetRequiredService<IBrandRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _brandRepository.GetListAsync(
                    name: "4f87a56ff73c485ea7b16af069743fe97347b"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("3fc80ffa-3435-464d-b377-00b043fa4b6f"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _brandRepository.GetCountAsync(
                    name: "ef777194580547ba852c694821c4e7c4aba51efe0fc54b21a9df69f11b23db0981fc2a65275e49089f1613ee6f7bd894"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}