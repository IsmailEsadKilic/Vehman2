using Vehman2.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Vehman2;

[DependsOn(
    typeof(Vehman2EntityFrameworkCoreTestModule)
    )]
public class Vehman2DomainTestModule : AbpModule
{

}
