using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using Vehman2.Owners;
using Vehman2.EntityFrameworkCore;
using Xunit;

namespace Vehman2.Owners
{
    public class OwnerRepositoryTests : Vehman2EntityFrameworkCoreTestBase
    {
        private readonly IOwnerRepository _ownerRepository;

        public OwnerRepositoryTests()
        {
            _ownerRepository = GetRequiredService<IOwnerRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _ownerRepository.GetListAsync(
                    name: "28db860b95634be8b0d6cb99327908e3c9c0585b04844469aa70d1c847b872283c928e20603447629"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("9107552d-e084-473e-ba34-f674f8509e9b"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _ownerRepository.GetCountAsync(
                    name: "fe5004b7"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}