using System.Reflection;
using FluentValidation;
using gridbase.Application.Common.Behaviors;
using gridbase.Application.Common.Behaviours;
using gridbase.Application.Common.Services;
using gridbase.Application.Services;
using gridbase.Domain.Common;
using gridbase.Application.Services.Interfaces;
using gridbase.Infrastructure.Context;
using gridbase.Application.Common.Interfaces;
using gridbase.Application.Interfaces;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddTransient<IGridBaseService, GridBaseService>();
        services.AddTransient<IDatatableService, DatatableService>();
        services.AddTransient<ITableColumnService, TableColumnService>();
        services.AddTransient<ITableRowService, TableRowService>();
        services.AddTransient<ITableCellService, TableCellService>();
        services.AddTransient<IMenuItemService, MenuItemService>();
        services.AddTransient<IBadgeService, BadgeService>();

        services.AddScoped<ITenantConfigService, TenantConfigService>();
        services.AddScoped<ILandingHeroService, LandingHeroService>();
        services.AddScoped<ILandingFeaturesService, LandingFeaturesService>();
        services.AddScoped<ICompanyProjectService, CompanyProjectService>();
        services.AddScoped<IClientItemService, ClientItemService>();
        services.AddScoped<ITestimonialService, TestimonialService>();
        services.AddScoped<IServiceSectionService, ServiceSectionService>();
        services.AddScoped<ISocialLinkService, SocialLinkService>();
        services.AddScoped<IContactService, ContactService>();
        services.AddScoped<IBrandService, BrandService>();
        services.AddScoped<IStatsSectionService, StatsSectionService>();
        services.AddScoped<IFaqService, FaqService>();
        services.AddScoped<IPlanService, PlanService>();
        services.AddScoped<IGalleryItemService, GalleryItemService>();
        services.AddScoped<IDocumentService, DocumentService>();
        services.AddScoped<IAboutConfigService, AboutConfigService>();
        services.AddScoped<MenuSnapshotService>();
        services.AddScoped<ITableAccessGuard, TableAccessGuard>();
        services.AddScoped<IProjectService, ProjectService>();
        services.AddScoped<IProjectContext, ProjectContext>();
        services.AddScoped<IApiKeyService, ApiKeyService>();
        services.AddScoped<IProjectCorsService, ProjectCorsService>();


        services.AddTransient<IMongoDbService, MongoDbService>();

        services.AddScoped<IScopedProcessingService, ProcessingService>();

        services.AddScoped(typeof(IService<,>), typeof(BaseService<,>));

        var assembly = Assembly.GetExecutingAssembly();

        // AutoMapper
        services.AddAutoMapper(assembly);

        // Fluent Validation
        services.AddValidatorsFromAssembly(assembly);

        // MediatR
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(assembly);

            cfg.AddOpenBehavior(typeof(UnhandledExceptionBehaviour<,>));

            cfg.AddOpenBehavior(typeof(TableAccessBehavior<,>));

            cfg.AddOpenBehavior(typeof(PerformanceBehaviour<,>));

            cfg.AddOpenBehavior(typeof(ValidationBehaviour<,>));

            cfg.AddOpenBehavior(typeof(TransactionBehavior<,>));

        });

        return services;
    }
}
