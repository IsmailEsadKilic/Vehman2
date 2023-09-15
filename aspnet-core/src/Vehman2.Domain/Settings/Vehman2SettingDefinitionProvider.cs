using Volo.Abp.Settings;

namespace Vehman2.Settings;

public class Vehman2SettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(Vehman2Settings.MySetting1));
    }
}
