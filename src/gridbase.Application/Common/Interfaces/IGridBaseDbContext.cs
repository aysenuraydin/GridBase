
using Microsoft.EntityFrameworkCore;
using gridbase.Domain.Entities;

namespace gridbase.Application.Common.Interfaces;

public interface IGridBaseDbContext
{
    DbSet<MenuItem> MenuItems { get; }
    DbSet<MenuSnapshot> MenuSnapshots { get; }
    DbSet<Badge> Badges { get; }

    DbSet<Datatable> Datatables { get; }
    DbSet<TableColumn> TableColumns { get; }
    DbSet<TableRow> TableRows { get; }
    DbSet<TableCell> TableCells { get; }
    DbSet<User> Users { get; } //sil
    DbSet<ForeignTable> ForeignTables { get; set; }
    DbSet<OutboxMessage> OutboxMessages { get; set; }
    DbSet<TenantConfig> TenantConfigs { get; set; }

    DbSet<BrandConfig> BrandConfigs { get; }
    DbSet<ContactConfig> ContactConfigs { get; }
    DbSet<SocialLink> SocialLinks { get; }
    DbSet<TeamMember> TeamMembers { get; }
    DbSet<ServiceSection> ServiceSections { get; }
    DbSet<ServiceItem> ServiceItems { get; }
    DbSet<PlanSection> PlanSections { get; }
    DbSet<PlanItem> PlanItems { get; }
    DbSet<PlanFeature> PlanFeatures { get; }
    DbSet<FaqCategory> FaqCategories { get; }
    DbSet<FaqQuestion> FaqQuestions { get; }
    DbSet<StatsSection> StatsSections { get; }
    DbSet<Testimonial> Testimonials { get; }
    DbSet<Work> Works { get; }
    DbSet<ClientItem> ClientItems { get; }

    DbSet<FeatureItem> FeatureItems { get; }
    DbSet<FeatureDetail> FeatureDetails { get; }
    DbSet<CtaConfig> CtaConfigs { get; }

    DbSet<LandingHeroConfig> LandingHeroConfigs { get; }
    DbSet<HeroSliderImage> HeroSliderImages { get; }
    DbSet<GalleryItem> GalleryItems { get; }
    DbSet<AboutConfig> AboutConfigs { get; }
    DbSet<Document> Documents { get; }
    DbSet<Project> Projects { get; }
    public DbSet<ProjectCorsOrigin> ProjectCorsOrigins { get; }

    public DbSet<ApiKey> ApiKeys { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}