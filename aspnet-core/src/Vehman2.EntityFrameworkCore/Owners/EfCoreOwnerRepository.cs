using Vehman2.Companies;
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

namespace Vehman2.Owners
{
    public class EfCoreOwnerRepository : EfCoreRepository<Vehman2DbContext, Owner, Guid>, IOwnerRepository
    {
        public EfCoreOwnerRepository(IDbContextProvider<Vehman2DbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<OwnerWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync();

            return (await GetDbSetAsync()).Where(b => b.Id == id)
                .Select(owner => new OwnerWithNavigationProperties
                {
                    Owner = owner,
                    Company = dbContext.Set<Company>().FirstOrDefault(c => c.Id == owner.CompanyId)
                }).FirstOrDefault();
        }

        public async Task<List<OwnerWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            string name = null,
            Guid? companyId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, name, companyId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? OwnerConsts.GetDefaultSorting(true) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        protected virtual async Task<IQueryable<OwnerWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
        {
            return from owner in (await GetDbSetAsync())
                   join company in (await GetDbContextAsync()).Set<Company>() on owner.CompanyId equals company.Id into companies
                   from company in companies.DefaultIfEmpty()
                   select new OwnerWithNavigationProperties
                   {
                       Owner = owner,
                       Company = company
                   };
        }

        protected virtual IQueryable<OwnerWithNavigationProperties> ApplyFilter(
            IQueryable<OwnerWithNavigationProperties> query,
            string filterText,
            string name = null,
            Guid? companyId = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Owner.Name.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(name), e => e.Owner.Name.Contains(name))
                    .WhereIf(companyId != null && companyId != Guid.Empty, e => e.Company != null && e.Company.Id == companyId);
        }

        public async Task<List<Owner>> GetListAsync(
            string filterText = null,
            string name = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, name);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? OwnerConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            string name = null,
            Guid? companyId = null,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, name, companyId);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<Owner> ApplyFilter(
            IQueryable<Owner> query,
            string filterText,
            string name = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Name.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(name), e => e.Name.Contains(name));
        }
    }
}