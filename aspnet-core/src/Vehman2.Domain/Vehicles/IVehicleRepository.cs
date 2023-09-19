using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Vehman2.Vehicles
{
    public interface IVehicleRepository : IRepository<Vehicle, Guid>
    {
        Task<VehicleWithNavigationProperties> GetWithNavigationPropertiesAsync(
    Guid id,
    CancellationToken cancellationToken = default
);

        Task<List<VehicleWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            string plate = null,
            Guid? carModelId = null,
            Guid? fuelId = null,
            Guid? ownerId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<List<Vehicle>> GetListAsync(
                    string filterText = null,
                    string plate = null,
                    string sorting = null,
                    int maxResultCount = int.MaxValue,
                    int skipCount = 0,
                    CancellationToken cancellationToken = default
                );

        Task<long> GetCountAsync(
            string filterText = null,
            string plate = null,
            Guid? carModelId = null,
            Guid? fuelId = null,
            Guid? ownerId = null,
            CancellationToken cancellationToken = default);
    }
}