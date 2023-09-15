using System.Threading.Tasks;

namespace Vehman2.Data;

public interface IVehman2DbSchemaMigrator
{
    Task MigrateAsync();
}
