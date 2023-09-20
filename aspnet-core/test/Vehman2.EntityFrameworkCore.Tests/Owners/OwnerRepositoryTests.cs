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
                    name: "7deff88ab28f4bcc94e48ae36b49bf4"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("3d73a488-c5cb-4921-b477-00248e97f36a"));
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
                    name: "6be0de4714c347169af0e18259e4106cb9302ffcb07c46528d8f060827faea398595817be0194ae3afd9bb9a96555735c1"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}