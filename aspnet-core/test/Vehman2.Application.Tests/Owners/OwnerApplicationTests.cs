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
            result.Items.Any(x => x.Id == Guid.Parse("a7b90ad5-686d-4c4e-84fb-cb786c5cd180")).ShouldBe(true);
            result.Items.Any(x => x.Id == Guid.Parse("d568ca04-9dac-47ac-81c2-03037efc67d7")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _ownersAppService.GetAsync(Guid.Parse("a7b90ad5-686d-4c4e-84fb-cb786c5cd180"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("a7b90ad5-686d-4c4e-84fb-cb786c5cd180"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new OwnerCreateDto
            {
                Name = "56b97799987242aab7a36082b80e3736592f63e5562140"
            };

            // Act
            var serviceResult = await _ownersAppService.CreateAsync(input);

            // Assert
            var result = await _ownerRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Name.ShouldBe("56b97799987242aab7a36082b80e3736592f63e5562140");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new OwnerUpdateDto()
            {
                Name = "891a1284307e4f4da4ba9e98d614269248ad7081e05f487292fa651bf5460a46ee775fa2b09b46"
            };

            // Act
            var serviceResult = await _ownersAppService.UpdateAsync(Guid.Parse("a7b90ad5-686d-4c4e-84fb-cb786c5cd180"), input);

            // Assert
            var result = await _ownerRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Name.ShouldBe("891a1284307e4f4da4ba9e98d614269248ad7081e05f487292fa651bf5460a46ee775fa2b09b46");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _ownersAppService.DeleteAsync(Guid.Parse("a7b90ad5-686d-4c4e-84fb-cb786c5cd180"));

            // Assert
            var result = await _ownerRepository.FindAsync(c => c.Id == Guid.Parse("a7b90ad5-686d-4c4e-84fb-cb786c5cd180"));

            result.ShouldBeNull();
        }
    }
}