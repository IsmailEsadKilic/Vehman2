using Vehman2.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Vehman2.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class Vehman2Controller : AbpControllerBase
{
    protected Vehman2Controller()
    {
        LocalizationResource = typeof(Vehman2Resource);
    }
}
