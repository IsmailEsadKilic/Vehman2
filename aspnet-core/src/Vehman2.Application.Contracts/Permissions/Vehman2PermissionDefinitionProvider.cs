using Vehman2.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;

namespace Vehman2.Permissions;

public class Vehman2PermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(Vehman2Permissions.GroupName);

        myGroup.AddPermission(Vehman2Permissions.Dashboard.Host, L("Permission:Dashboard"), MultiTenancySides.Host);
        myGroup.AddPermission(Vehman2Permissions.Dashboard.Tenant, L("Permission:Dashboard"), MultiTenancySides.Tenant);

        //Define your own permissions here. Example:
        //myGroup.AddPermission(Vehman2Permissions.MyPermission1, L("Permission:MyPermission1"));

        var fuelPermission = myGroup.AddPermission(Vehman2Permissions.Fuels.Default, L("Permission:Fuels"));
        fuelPermission.AddChild(Vehman2Permissions.Fuels.Create, L("Permission:Create"));
        fuelPermission.AddChild(Vehman2Permissions.Fuels.Edit, L("Permission:Edit"));
        fuelPermission.AddChild(Vehman2Permissions.Fuels.Delete, L("Permission:Delete"));

        var ownerPermission = myGroup.AddPermission(Vehman2Permissions.Owners.Default, L("Permission:Owners"));
        ownerPermission.AddChild(Vehman2Permissions.Owners.Create, L("Permission:Create"));
        ownerPermission.AddChild(Vehman2Permissions.Owners.Edit, L("Permission:Edit"));
        ownerPermission.AddChild(Vehman2Permissions.Owners.Delete, L("Permission:Delete"));

        var carModelPermission = myGroup.AddPermission(Vehman2Permissions.CarModels.Default, L("Permission:CarModels"));
        carModelPermission.AddChild(Vehman2Permissions.CarModels.Create, L("Permission:Create"));
        carModelPermission.AddChild(Vehman2Permissions.CarModels.Edit, L("Permission:Edit"));
        carModelPermission.AddChild(Vehman2Permissions.CarModels.Delete, L("Permission:Delete"));

        var brandPermission = myGroup.AddPermission(Vehman2Permissions.Brands.Default, L("Permission:Brands"));
        brandPermission.AddChild(Vehman2Permissions.Brands.Create, L("Permission:Create"));
        brandPermission.AddChild(Vehman2Permissions.Brands.Edit, L("Permission:Edit"));
        brandPermission.AddChild(Vehman2Permissions.Brands.Delete, L("Permission:Delete"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<Vehman2Resource>(name);
    }
}