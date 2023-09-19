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
            result.Items.Any(x => x.Owner.Id == Guid.Parse("9107552d-e084-473e-ba34-f674f8509e9b")).ShouldBe(true);
            result.Items.Any(x => x.Owner.Id == Guid.Parse("a48e770b-d759-4487-ab3f-29f6002c9775")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _ownersAppService.GetAsync(Guid.Parse("9107552d-e084-473e-ba34-f674f8509e9b"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("9107552d-e084-473e-ba34-f674f8509e9b"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new OwnerCreateDto
            {
                Name = "f50dc4be6cd04c1a969fdfbaafa1fedea85f8a809953405b81",
                CompanyId = Guid.Parse("4c1ffebb-1f01-4052-aabe-d2406b44aaf1")
            };

            // Act
            var serviceResult = await _ownersAppService.CreateAsync(input);

            // Assert
            var result = await _ownerRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Name.ShouldBe("f50dc4be6cd04c1a969fdfbaafa1fedea85f8a809953405b81");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new OwnerUpdateDto()
            {
                Name = "2aab28919afe",
                CompanyId = Guid.Parse("4c1ffebb-1f01-4052-aabe-d2406b44aaf1")
            };

            // Act
            var serviceResult = await _ownersAppService.UpdateAsync(Guid.Parse("9107552d-e084-473e-ba34-f674f8509e9b"), input);

            // Assert
            var result = await _ownerRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Name.ShouldBe("2aab28919afe");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _ownersAppService.DeleteAsync(Guid.Parse("9107552d-e084-473e-ba34-f674f8509e9b"));

            // Assert
            var result = await _ownerRepository.FindAsync(c => c.Id == Guid.Parse("9107552d-e084-473e-ba34-f674f8509e9b"));

            result.ShouldBeNull();
        }
    }
}