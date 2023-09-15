using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Vehman2.Data;

/* This is used if database provider does't define
 * IVehman2DbSchemaMigrator implementation.
 */
public class NullVehman2DbSchemaMigrator : IVehman2DbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
