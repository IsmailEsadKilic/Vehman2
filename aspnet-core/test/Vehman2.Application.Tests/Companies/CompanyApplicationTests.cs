using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace Vehman2.Companies
{
    public class CompaniesAppServiceTests : Vehman2ApplicationTestBase
    {
        private readonly ICompaniesAppService _companiesAppService;
        private readonly IRepository<Company, Guid> _companyRepository;

        public CompaniesAppServiceTests()
        {
            _companiesAppService = GetRequiredService<ICompaniesAppService>();
            _companyRepository = GetRequiredService<IRepository<Company, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _companiesAppService.GetListAsync(new GetCompaniesInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.Id == Guid.Parse("4c1ffebb-1f01-4052-aabe-d2406b44aaf1")).ShouldBe(true);
            result.Items.Any(x => x.Id == Guid.Parse("91d7dff0-1d58-44b2-a06e-2f3d981284c5")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _companiesAppService.GetAsync(Guid.Parse("4c1ffebb-1f01-4052-aabe-d2406b44aaf1"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("4c1ffebb-1f01-4052-aabe-d2406b44aaf1"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new CompanyCreateDto
            {
                Name = "eccd130d7ae04dc98be7edc0fc87847a4fffe8b787834cbca08c7577848cc08b0e0a8c4547834b6788"
            };

            // Act
            var serviceResult = await _companiesAppService.CreateAsync(input);

            // Assert
            var result = await _companyRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Name.ShouldBe("eccd130d7ae04dc98be7edc0fc87847a4fffe8b787834cbca08c7577848cc08b0e0a8c4547834b6788");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new CompanyUpdateDto()
            {
                Name = "f0ce7ccbd8a74b8c8bf4e0a447ca3f7605df9a3659c24537ab0ad28cafd"
            };

            // Act
            var serviceResult = await _companiesAppService.UpdateAsync(Guid.Parse("4c1ffebb-1f01-4052-aabe-d2406b44aaf1"), input);

            // Assert
            var result = await _companyRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Name.ShouldBe("f0ce7ccbd8a74b8c8bf4e0a447ca3f7605df9a3659c24537ab0ad28cafd");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _companiesAppService.DeleteAsync(Guid.Parse("4c1ffebb-1f01-4052-aabe-d2406b44aaf1"));

            // Assert
            var result = await _companyRepository.FindAsync(c => c.Id == Guid.Parse("4c1ffebb-1f01-4052-aabe-d2406b44aaf1"));

            result.ShouldBeNull();
        }
    }
}