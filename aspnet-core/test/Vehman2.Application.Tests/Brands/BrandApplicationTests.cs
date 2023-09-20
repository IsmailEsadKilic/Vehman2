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
            result.Items.Any(x => x.Id == Guid.Parse("3fc80ffa-3435-464d-b377-00b043fa4b6f")).ShouldBe(true);
            result.Items.Any(x => x.Id == Guid.Parse("d5186959-9314-4ed4-9a06-4d005a619bbc")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _brandsAppService.GetAsync(Guid.Parse("3fc80ffa-3435-464d-b377-00b043fa4b6f"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("3fc80ffa-3435-464d-b377-00b043fa4b6f"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new BrandCreateDto
            {
                Name = "02119b6d7955432c82df16c28917af80330722f9e6b548ce8086a1f07a"
            };

            // Act
            var serviceResult = await _brandsAppService.CreateAsync(input);

            // Assert
            var result = await _brandRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Name.ShouldBe("02119b6d7955432c82df16c28917af80330722f9e6b548ce8086a1f07a");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new BrandUpdateDto()
            {
                Name = "03fa5feda0f1465aa71616d6ba11"
            };

            // Act
            var serviceResult = await _brandsAppService.UpdateAsync(Guid.Parse("3fc80ffa-3435-464d-b377-00b043fa4b6f"), input);

            // Assert
            var result = await _brandRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Name.ShouldBe("03fa5feda0f1465aa71616d6ba11");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _brandsAppService.DeleteAsync(Guid.Parse("3fc80ffa-3435-464d-b377-00b043fa4b6f"));

            // Assert
            var result = await _brandRepository.FindAsync(c => c.Id == Guid.Parse("3fc80ffa-3435-464d-b377-00b043fa4b6f"));

            result.ShouldBeNull();
        }
    }
}