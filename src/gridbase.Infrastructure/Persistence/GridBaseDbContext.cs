using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using gridbase.Application.Common.Interfaces;
using gridbase.Domain.Entities;
using gridbase.Infrastructure.Identity;

namespace gridbase.Infrastructure.Persistence;

public class GridBaseDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>, IGridBaseDbContext
{
    public GridBaseDbContext(DbContextOptions<GridBaseDbContext> options) : base(options)
    {
    }
    public DbSet<MenuItem> MenuItems { get; set; }
    public DbSet<MenuSnapshot> MenuSnapshots { get; set; }
    public DbSet<Badge> Badges { get; set; }


    public DbSet<Datatable> Datatables { get; set; }

    public DbSet<TableColumn> TableColumns { get; set; }

    public DbSet<TableRow> TableRows { get; set; }

    public DbSet<TableCell> TableCells { get; set; }

    public DbSet<ForeignTable> ForeignTables { get; set; }

    public DbSet<User> Users { get; set; }
    public DbSet<OutboxMessage> OutboxMessages { get; set; }
    public DbSet<TenantConfig> TenantConfigs { get; set; }


    public DbSet<BrandConfig> BrandConfigs => Set<BrandConfig>();
    public DbSet<ContactConfig> ContactConfigs => Set<ContactConfig>();
    public DbSet<SocialLink> SocialLinks => Set<SocialLink>();
    public DbSet<TeamMember> TeamMembers => Set<TeamMember>();
    public DbSet<ServiceSection> ServiceSections => Set<ServiceSection>();
    public DbSet<ServiceItem> ServiceItems => Set<ServiceItem>();
    public DbSet<PlanSection> PlanSections => Set<PlanSection>();
    public DbSet<PlanItem> PlanItems => Set<PlanItem>();
    public DbSet<PlanFeature> PlanFeatures => Set<PlanFeature>();
    public DbSet<FaqCategory> FaqCategories => Set<FaqCategory>();
    public DbSet<FaqQuestion> FaqQuestions => Set<FaqQuestion>();
    public DbSet<StatsSection> StatsSections => Set<StatsSection>();
    public DbSet<Testimonial> Testimonials => Set<Testimonial>();
    public DbSet<Work> Works => Set<Work>();
    public DbSet<ClientItem> ClientItems => Set<ClientItem>();

    public DbSet<FeatureItem> FeatureItems => Set<FeatureItem>();
    public DbSet<FeatureDetail> FeatureDetails => Set<FeatureDetail>();
    public DbSet<CtaConfig> CtaConfigs => Set<CtaConfig>();

    public DbSet<LandingHeroConfig> LandingHeroConfigs => Set<LandingHeroConfig>();
    public DbSet<HeroSliderImage> HeroSliderImages => Set<HeroSliderImage>();

    public DbSet<GalleryItem> GalleryItems => Set<GalleryItem>();
    public DbSet<Document> Documents => Set<Document>();
    public DbSet<AboutConfig> AboutConfigs => Set<AboutConfig>();
    public DbSet<Project> Projects => Set<Project>();
    public DbSet<ApiKey> ApiKeys => Set<ApiKey>();
    public DbSet<ProjectCorsOrigin> ProjectCorsOrigins => Set<ProjectCorsOrigin>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(GridBaseDbContext).Assembly);
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //     var cs = "Data Source=gridbase.db";
        //     optionsBuilder.UseSqlite(cs);

    }
}