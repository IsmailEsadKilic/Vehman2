using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace Vehman2;

[Dependency(ReplaceServices = true)]
public class Vehman2BrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "Vehman2";
}
