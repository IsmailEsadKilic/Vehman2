using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Vehman2.Data;
using Volo.Abp.DependencyInjection;

namespace Vehman2.EntityFrameworkCore;

public class EntityFrameworkCoreVehman2DbSchemaMigrator
    : IVehman2DbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreVehman2DbSchemaMigrator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolving the Vehman2DbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<Vehman2DbContext>()
            .Database
            .MigrateAsync();
    }
}
