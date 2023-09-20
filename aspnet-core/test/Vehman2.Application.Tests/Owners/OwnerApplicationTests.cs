using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace Vehman2.Owners
{
    public class OwnersAppServiceTests : Vehman2ApplicationTestBase
    {
        private readonly IOwnersAppService _ownersAppService;
        private readonly IRepository<Owner, Guid> _ownerRepository;

        public OwnersAppServiceTests()
        {
            _ownersAppService = GetRequiredService<IOwnersAppService>();
            _ownerRepository = GetRequiredService<IRepository<Owner, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _ownersAppService.GetListAsync(new GetOwnersInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.Owner.Id == Guid.Parse("3d73a488-c5cb-4921-b477-00248e97f36a")).ShouldBe(true);
            result.Items.Any(x => x.Owner.Id == Guid.Parse("1016182a-5752-4509-9c5f-47c4afece02b")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _ownersAppService.GetAsync(Guid.Parse("3d73a488-c5cb-4921-b477-00248e97f36a"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("3d73a488-c5cb-4921-b477-00248e97f36a"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new OwnerCreateDto
            {
                Name = "ace9e18239094d",
                CompanyId = Guid.Parse("7f1e47d3-0d94-45e1-8cf7-794286df8526")
            };

            // Act
            var serviceResult = await _ownersAppService.CreateAsync(input);

            // Assert
            var result = await _ownerRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Name.ShouldBe("ace9e18239094d");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new OwnerUpdateDto()
            {
                Name = "dc1ce05c83fa4b8db493cce1479d6706a95d6b",
                CompanyId = Guid.Parse("7f1e47d3-0d94-45e1-8cf7-794286df8526")
            };

            // Act
            var serviceResult = await _ownersAppService.UpdateAsync(Guid.Parse("3d73a488-c5cb-4921-b477-00248e97f36a"), input);

            // Assert
            var result = await _ownerRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Name.ShouldBe("dc1ce05c83fa4b8db493cce1479d6706a95d6b");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _ownersAppService.DeleteAsync(Guid.Parse("3d73a488-c5cb-4921-b477-00248e97f36a"));

            // Assert
            var result = await _ownerRepository.FindAsync(c => c.Id == Guid.Parse("3d73a488-c5cb-4921-b477-00248e97f36a"));

            result.ShouldBeNull();
        }
    }
}