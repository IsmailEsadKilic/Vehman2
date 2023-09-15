using Vehman2.Localization;
using Volo.Abp.Application.Services;

namespace Vehman2;

/* Inherit your application services from this class.
 */
public abstract class Vehman2AppService : ApplicationService
{
    protected Vehman2AppService()
    {
        LocalizationResource = typeof(Vehman2Resource);
    }
}
