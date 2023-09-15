using Vehman2.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace Vehman2.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(Vehman2EntityFrameworkCoreModule),
    typeof(Vehman2ApplicationContractsModule)
)]
public class Vehman2DbMigratorModule : AbpModule
{
}
