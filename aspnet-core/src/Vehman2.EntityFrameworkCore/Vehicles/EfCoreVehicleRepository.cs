using Vehman2.Owners;
using Vehman2.Fuels;
using Vehman2.CarModels;
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

namespace Vehman2.Vehicles
{
    public class EfCoreVehicleRepository : EfCoreRepository<Vehman2DbContext, Vehicle, Guid>, IVehicleRepository
    {
        public EfCoreVehicleRepository(IDbContextProvider<Vehman2DbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<VehicleWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync();

            return (await GetDbSetAsync()).Where(b => b.Id == id)
                .Select(vehicle => new VehicleWithNavigationProperties
                {
                    Vehicle = vehicle,
                    CarModel = dbContext.Set<CarModel>().FirstOrDefault(c => c.Id == vehicle.CarModelId),
                    Fuel = dbContext.Set<Fuel>().FirstOrDefault(c => c.Id == vehicle.FuelId),
                    Owner = dbContext.Set<Owner>().FirstOrDefault(c => c.Id == vehicle.OwnerId)
                }).FirstOrDefault();
        }

        public async Task<List<VehicleWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            string plate = null,
            Guid? carModelId = null,
            Guid? fuelId = null,
            Guid? ownerId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, plate, carModelId, fuelId, ownerId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? VehicleConsts.GetDefaultSorting(true) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        protected virtual async Task<IQueryable<VehicleWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
        {
            return from vehicle in (await GetDbSetAsync())
                   join carModel in (await GetDbContextAsync()).Set<CarModel>() on vehicle.CarModelId equals carModel.Id into carModels
                   from carModel in carModels.DefaultIfEmpty()
                   join fuel in (await GetDbContextAsync()).Set<Fuel>() on vehicle.FuelId equals fuel.Id into fuels
                   from fuel in fuels.DefaultIfEmpty()
                   join owner in (await GetDbContextAsync()).Set<Owner>() on vehicle.OwnerId equals owner.Id into owners
                   from owner in owners.DefaultIfEmpty()
                   select new VehicleWithNavigationProperties
                   {
                       Vehicle = vehicle,
                       CarModel = carModel,
                       Fuel = fuel,
                       Owner = owner
                   };
        }

        protected virtual IQueryable<VehicleWithNavigationProperties> ApplyFilter(
            IQueryable<VehicleWithNavigationProperties> query,
            string filterText,
            string plate = null,
            Guid? carModelId = null,
            Guid? fuelId = null,
            Guid? ownerId = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Vehicle.Plate.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(plate), e => e.Vehicle.Plate.Contains(plate))
                    .WhereIf(carModelId != null && carModelId != Guid.Empty, e => e.CarModel != null && e.CarModel.Id == carModelId)
                    .WhereIf(fuelId != null && fuelId != Guid.Empty, e => e.Fuel != null && e.Fuel.Id == fuelId)
                    .WhereIf(ownerId != null && ownerId != Guid.Empty, e => e.Owner != null && e.Owner.Id == ownerId);
        }

        public async Task<List<Vehicle>> GetListAsync(
            string filterText = null,
            string plate = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, plate);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? VehicleConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            string plate = null,
            Guid? carModelId = null,
            Guid? fuelId = null,
            Guid? ownerId = null,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, plate, carModelId, fuelId, ownerId);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<Vehicle> ApplyFilter(
            IQueryable<Vehicle> query,
            string filterText,
            string plate = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Plate.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(plate), e => e.Plate.Contains(plate));
        }
    }
}