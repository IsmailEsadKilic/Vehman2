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
            result.Items.Any(x => x.Id == Guid.Parse("7f1e47d3-0d94-45e1-8cf7-794286df8526")).ShouldBe(true);
            result.Items.Any(x => x.Id == Guid.Parse("96407a81-8d2c-42c5-8072-ca35461a2be8")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _companiesAppService.GetAsync(Guid.Parse("7f1e47d3-0d94-45e1-8cf7-794286df8526"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("7f1e47d3-0d94-45e1-8cf7-794286df8526"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new CompanyCreateDto
            {
                Name = "b658c6e7a6c34407b00a4f8b9b839a88a2e87e9fbb1b4eadbfcd2b9fbf72753f2d6cfca60d3642c5"
            };

            // Act
            var serviceResult = await _companiesAppService.CreateAsync(input);

            // Assert
            var result = await _companyRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Name.ShouldBe("b658c6e7a6c34407b00a4f8b9b839a88a2e87e9fbb1b4eadbfcd2b9fbf72753f2d6cfca60d3642c5");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new CompanyUpdateDto()
            {
                Name = "2a6cdcd896454362ade071556ab7089659448e943f6b4372ad29f22b3fc4ac13ad4804bb30f24f34be5443f8613051942"
            };

            // Act
            var serviceResult = await _companiesAppService.UpdateAsync(Guid.Parse("7f1e47d3-0d94-45e1-8cf7-794286df8526"), input);

            // Assert
            var result = await _companyRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Name.ShouldBe("2a6cdcd896454362ade071556ab7089659448e943f6b4372ad29f22b3fc4ac13ad4804bb30f24f34be5443f8613051942");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _companiesAppService.DeleteAsync(Guid.Parse("7f1e47d3-0d94-45e1-8cf7-794286df8526"));

            // Assert
            var result = await _companyRepository.FindAsync(c => c.Id == Guid.Parse("7f1e47d3-0d94-45e1-8cf7-794286df8526"));

            result.ShouldBeNull();
        }
    }
}