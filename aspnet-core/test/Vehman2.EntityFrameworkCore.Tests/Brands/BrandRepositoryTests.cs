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
                    name: "99c9e928970c"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("565f707e-f160-401e-982c-b479e72458db"));
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
                    name: "16b6febf9e544871ac204e226b5f39e45fd39ab78b27476196e0dec92ebfaf168449f7069c3247afbf0d6e0b84164a299f"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}