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
                    name: "0146d277bb624639b65b8c0719eebdf42640620ebd5f4c63baab5708bec64e0a38cdf47e96f64b70a8510458eba"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("4c1ffebb-1f01-4052-aabe-d2406b44aaf1"));
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
                    name: "61733f4e7b504e8aa608edfc4bde905a5690e265174e48a6b92470944206d52f3"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}