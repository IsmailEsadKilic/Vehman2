using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using Vehman2.Companies;
using Vehman2.EntityFrameworkCore;
using Xunit;

namespace Vehman2.Companies
{
    public class CompanyRepositoryTests : Vehman2EntityFrameworkCoreTestBase
    {
        private readonly ICompanyRepository _companyRepository;

        public CompanyRepositoryTests()
        {
            _companyRepository = GetRequiredService<ICompanyRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _companyRepository.GetListAsync(
                    name: "cf726c853870419a90e7501f38b0e79aa72aafb0d951477da6e"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("7f1e47d3-0d94-45e1-8cf7-794286df8526"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _companyRepository.GetCountAsync(
                    name: "6f42d4b18c6d49fbabc198f9d12a24bf6a6cb006c7"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}