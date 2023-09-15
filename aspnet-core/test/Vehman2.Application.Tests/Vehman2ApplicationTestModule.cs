using Volo.Abp.Modularity;

namespace Vehman2;

[DependsOn(
    typeof(Vehman2ApplicationModule),
    typeof(Vehman2DomainTestModule)
    )]
public class Vehman2ApplicationTestModule : AbpModule
{

}
