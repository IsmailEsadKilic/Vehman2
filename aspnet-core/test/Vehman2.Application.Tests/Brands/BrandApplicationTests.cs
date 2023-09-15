using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace Vehman2.Brands
{
    public class BrandsAppServiceTests : Vehman2ApplicationTestBase
    {
        private readonly IBrandsAppService _brandsAppService;
        private readonly IRepository<Brand, Guid> _brandRepository;

        public BrandsAppServiceTests()
        {
            _brandsAppService = GetRequiredService<IBrandsAppService>();
            _brandRepository = GetRequiredService<IRepository<Brand, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _brandsAppService.GetListAsync(new GetBrandsInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.Id == Guid.Parse("565f707e-f160-401e-982c-b479e72458db")).ShouldBe(true);
            result.Items.Any(x => x.Id == Guid.Parse("7a2d1d6b-ecce-4403-8223-19256c59cdb4")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _brandsAppService.GetAsync(Guid.Parse("565f707e-f160-401e-982c-b479e72458db"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("565f707e-f160-401e-982c-b479e72458db"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new BrandCreateDto
            {
                Name = "98b29aa0ecad46ef8ba6bad220d7a308e4f71c7725fa4ef38b35a907c57ce6ac83217d61acd84bc58ba3a0387965"
            };

            // Act
            var serviceResult = await _brandsAppService.CreateAsync(input);

            // Assert
            var result = await _brandRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Name.ShouldBe("98b29aa0ecad46ef8ba6bad220d7a308e4f71c7725fa4ef38b35a907c57ce6ac83217d61acd84bc58ba3a0387965");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new BrandUpdateDto()
            {
                Name = "f02489871be84704bfb42c3ba680c3e5c13000baede04a6bb4f8967427b5"
            };

            // Act
            var serviceResult = await _brandsAppService.UpdateAsync(Guid.Parse("565f707e-f160-401e-982c-b479e72458db"), input);

            // Assert
            var result = await _brandRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Name.ShouldBe("f02489871be84704bfb42c3ba680c3e5c13000baede04a6bb4f8967427b5");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _brandsAppService.DeleteAsync(Guid.Parse("565f707e-f160-401e-982c-b479e72458db"));

            // Assert
            var result = await _brandRepository.FindAsync(c => c.Id == Guid.Parse("565f707e-f160-401e-982c-b479e72458db"));

            result.ShouldBeNull();
        }
    }
}