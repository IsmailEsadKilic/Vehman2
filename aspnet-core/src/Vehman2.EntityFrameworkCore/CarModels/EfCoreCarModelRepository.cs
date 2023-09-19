using Vehman2.Brands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Vehman2.EntityFrameworkCore;

namespace Vehman2.CarModels
{
    public class EfCoreCarModelRepository : EfCoreRepository<Vehman2DbContext, CarModel, Guid>, ICarModelRepository
    {
        public EfCoreCarModelRepository(IDbContextProvider<Vehman2DbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<CarModelWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync();

            return (await GetDbSetAsync()).Where(b => b.Id == id)
                .Select(carModel => new CarModelWithNavigationProperties
                {
                    CarModel = carModel,
                    Brand = dbContext.Set<Brand>().FirstOrDefault(c => c.Id == carModel.BrandId)
                }).FirstOrDefault();
        }

        public async Task<List<CarModelWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            string name = null,
            Guid? brandId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, name, brandId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? CarModelConsts.GetDefaultSorting(true) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        protected virtual async Task<IQueryable<CarModelWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
        {
            return from carModel in (await GetDbSetAsync())
                   join brand in (await GetDbContextAsync()).Set<Brand>() on carModel.BrandId equals brand.Id into brands
                   from brand in brands.DefaultIfEmpty()
                   select new CarModelWithNavigationProperties
                   {
                       CarModel = carModel,
                       Brand = brand
                   };
        }

        protected virtual IQueryable<CarModelWithNavigationProperties> ApplyFilter(
            IQueryable<CarModelWithNavigationProperties> query,
            string filterText,
            string name = null,
            Guid? brandId = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.CarModel.Name.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(name), e => e.CarModel.Name.Contains(name))
                    .WhereIf(brandId != null && brandId != Guid.Empty, e => e.Brand != null && e.Brand.Id == brandId);
        }

        public async Task<List<CarModel>> GetListAsync(
            string filterText = null,
            string name = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, name);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? CarModelConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            string name = null,
            Guid? brandId = null,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, name, brandId);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<CarModel> ApplyFilter(
            IQueryable<CarModel> query,
            string filterText,
            string name = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Name.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(name), e => e.Name.Contains(name));
        }
    }
}