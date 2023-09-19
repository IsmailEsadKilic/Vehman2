using Vehman2.Transactions;
using Vehman2.Vehicles;
using Vehman2.Companies;
using Vehman2.Brands;
using Vehman2.CarModels;
using Vehman2.Owners;
using Vehman2.Fuels;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.BlobStoring.Database.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.LanguageManagement.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TextTemplateManagement.EntityFrameworkCore;
using Volo.Saas.EntityFrameworkCore;
using Volo.Saas.Editions;
using Volo.Saas.Tenants;
using Volo.Abp.Gdpr;
using Volo.Abp.OpenIddict.EntityFrameworkCore;

namespace Vehman2.EntityFrameworkCore;

[ReplaceDbContext(typeof(IIdentityProDbContext))]
[ReplaceDbContext(typeof(ISaasDbContext))]
[ConnectionStringName("Default")]
public class Vehman2DbContext :
    AbpDbContext<Vehman2DbContext>,
    IIdentityProDbContext,
    ISaasDbContext
{
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<Vehicle> Vehicles { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<Brand> Brands { get; set; }
    public DbSet<CarModel> CarModels { get; set; }
    public DbSet<Owner> Owners { get; set; }
    public DbSet<Fuel> Fuels { get; set; }
    /* Add DbSet properties for your Aggregate Roots / Entities here. */

    #region Entities from the modules

    /* Notice: We only implemented IIdentityProDbContext and ISaasDbContext
     * and replaced them for this DbContext. This allows you to perform JOIN
     * queries for the entities of these modules over the repositories easily. You
     * typically don't need that for other modules. But, if you need, you can
     * implement the DbContext interface of the needed module and use ReplaceDbContext
     * attribute just like IIdentityProDbContext and ISaasDbContext.
     *
     * More info: Replacing a DbContext of a module ensures that the related module
     * uses this DbContext on runtime. Otherwise, it will use its own DbContext class.
     */

    // Identity
    public DbSet<IdentityUser> Users { get; set; }
    public DbSet<IdentityRole> Roles { get; set; }
    public DbSet<IdentityClaimType> ClaimTypes { get; set; }
    public DbSet<OrganizationUnit> OrganizationUnits { get; set; }
    public DbSet<IdentitySecurityLog> SecurityLogs { get; set; }
    public DbSet<IdentityLinkUser> LinkUsers { get; set; }
    public DbSet<IdentityUserDelegation> UserDelegations { get; }

    // SaaS
    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<Edition> Editions { get; set; }
    public DbSet<TenantConnectionString> TenantConnectionStrings { get; set; }

    #endregion

    public Vehman2DbContext(DbContextOptions<Vehman2DbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        /* Include modules to your migration db context */

        builder.ConfigurePermissionManagement();
        builder.ConfigureSettingManagement();
        builder.ConfigureBackgroundJobs();
        builder.ConfigureAuditLogging();
        builder.ConfigureIdentityPro();
        builder.ConfigureOpenIddictPro();
        builder.ConfigureFeatureManagement();
        builder.ConfigureLanguageManagement();
        builder.ConfigureSaas();
        builder.ConfigureTextTemplateManagement();
        builder.ConfigureBlobStoring();
        builder.ConfigureGdpr();

        /* Configure your own tables/entities inside here */

        //builder.Entity<YourEntity>(b =>
        //{
        //    b.ToTable(Vehman2Consts.DbTablePrefix + "YourEntities", Vehman2Consts.DbSchema);
        //    b.ConfigureByConvention(); //auto configure for the base class props
        //    //...
        //});
        if (builder.IsHostDatabase())
        {
            builder.Entity<Fuel>(b =>
{
    b.ToTable(Vehman2Consts.DbTablePrefix + "Fuels", Vehman2Consts.DbSchema);
    b.ConfigureByConvention();
    b.Property(x => x.Name).HasColumnName(nameof(Fuel.Name)).IsRequired();
});

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {
            builder.Entity<Brand>(b =>
{
    b.ToTable(Vehman2Consts.DbTablePrefix + "Brands", Vehman2Consts.DbSchema);
    b.ConfigureByConvention();
    b.Property(x => x.Name).HasColumnName(nameof(Brand.Name)).IsRequired();
});

        }
        if (builder.IsHostDatabase())
        {
            builder.Entity<CarModel>(b =>
{
    b.ToTable(Vehman2Consts.DbTablePrefix + "CarModels", Vehman2Consts.DbSchema);
    b.ConfigureByConvention();
    b.Property(x => x.Name).HasColumnName(nameof(CarModel.Name)).IsRequired();
    b.HasOne<Brand>().WithMany().IsRequired().HasForeignKey(x => x.BrandId).OnDelete(DeleteBehavior.NoAction);
});

        }
        if (builder.IsHostDatabase())
        {
            builder.Entity<Company>(b =>
{
    b.ToTable(Vehman2Consts.DbTablePrefix + "Companies", Vehman2Consts.DbSchema);
    b.ConfigureByConvention();
    b.Property(x => x.Name).HasColumnName(nameof(Company.Name)).IsRequired();
});

        }
        if (builder.IsHostDatabase())
        {
            builder.Entity<Owner>(b =>
{
    b.ToTable(Vehman2Consts.DbTablePrefix + "Owners", Vehman2Consts.DbSchema);
    b.ConfigureByConvention();
    b.Property(x => x.Name).HasColumnName(nameof(Owner.Name)).IsRequired();
    b.HasOne<Company>().WithMany().IsRequired().HasForeignKey(x => x.CompanyId).OnDelete(DeleteBehavior.NoAction);
});

        }
        if (builder.IsHostDatabase())
        {
            builder.Entity<Vehicle>(b =>
{
    b.ToTable(Vehman2Consts.DbTablePrefix + "Vehicles", Vehman2Consts.DbSchema);
    b.ConfigureByConvention();
    b.Property(x => x.Plate).HasColumnName(nameof(Vehicle.Plate)).IsRequired();
    b.HasOne<CarModel>().WithMany().IsRequired().HasForeignKey(x => x.CarModelId).OnDelete(DeleteBehavior.NoAction);
    b.HasOne<Fuel>().WithMany().IsRequired().HasForeignKey(x => x.FuelId).OnDelete(DeleteBehavior.NoAction);
    b.HasOne<Owner>().WithMany().IsRequired().HasForeignKey(x => x.OwnerId).OnDelete(DeleteBehavior.NoAction);
});

        }
        if (builder.IsHostDatabase())
        {
            builder.Entity<Transaction>(b =>
{
    b.ToTable(Vehman2Consts.DbTablePrefix + "Transactions", Vehman2Consts.DbSchema);
    b.ConfigureByConvention();
    b.Property(x => x.Price).HasColumnName(nameof(Transaction.Price)).IsRequired().HasMaxLength((int)TransactionConsts.PriceMaxLength);
    b.Property(x => x.Liters).HasColumnName(nameof(Transaction.Liters)).HasMaxLength((int)TransactionConsts.LitersMaxLength);
    b.Property(x => x.Date).HasColumnName(nameof(Transaction.Date));
    b.HasOne<Vehicle>().WithMany().IsRequired().HasForeignKey(x => x.VehicleId).OnDelete(DeleteBehavior.NoAction);
});

        }
    }
}