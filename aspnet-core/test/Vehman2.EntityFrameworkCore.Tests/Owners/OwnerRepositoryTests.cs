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
                    name: "efba5a42b6a9441c96cfd610b26a3f2afb823c3d615e438a90d8c618dd9a3232d55d2e9c836f43ed903"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("a7b90ad5-686d-4c4e-84fb-cb786c5cd180"));
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
                    name: "be2af4b9a2c34305a8963"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}