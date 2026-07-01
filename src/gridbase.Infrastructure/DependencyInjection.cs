using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using gridbase.Application.Common.Interfaces;
using gridbase.Application.Common.Models.Email;
using gridbase.Application.Services.Interfaces;
using gridbase.Domain.Common;
using gridbase.Domain.Repositories;
using gridbase.Infrastructure.Constants;
using gridbase.Infrastructure.Identity;
using gridbase.Infrastructure.Identity.Services;
using gridbase.Infrastructure.Logging;
using gridbase.Infrastructure.Persistence;
using gridbase.Infrastructure.Persistence.Common;
using gridbase.Infrastructure.Persistence.Common.Repositories;
using gridbase.Infrastructure.Persistence.Interceptors;
using gridbase.Infrastructure.Persistence.Repositories;
using gridbase.Infrastructure.Services;
using gridbase.Application.Interfaces;
using RedisCache = gridbase.Infrastructure.Caching.RedisCache;
using gridbase.Infrastructure.Email;
using gridbase.Infrastructure.Persistence.Repositories;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        services.AddScoped<ISaveChangesInterceptor, HybridDomainEventInterceptor>();


        services.AddDbContext<GridBaseDbContext>((sp, options) =>
        {
            var interceptors = sp.GetServices<ISaveChangesInterceptor>();
            options.AddInterceptors(interceptors);

            options.UseSqlite(configuration.GetConnectionString(ConnectionSettings.DB_CONNECTION_KEY));
        });

        services.AddScoped<ISqlConnectionFactory>(provider =>
            new SqlConnectionFactory(configuration.GetConnectionString(ConnectionSettings.DB_CONNECTION_KEY)));

        services.AddScoped<IGridBaseDbContext>(provider => provider.GetRequiredService<GridBaseDbContext>());

        services.AddScoped<IGridBaseRepository, GridBaseRepository>();
        services.AddScoped<ITableRepository, TableRepository>();
        services.AddScoped<ITableColumnRepository, TableColumnRepository>();
        services.AddScoped<ITableCellRepository, TableCellRepository>();
        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<IColumnDataRepository, ColumnDataRepository>();
        services.AddScoped<IColumnUIRepository, ColumnUIRepository>();

        services.AddScoped<IValidationRepository, ValidationRepository>();
        services.AddScoped<IRulesRepository, RulesRepository>();

        services.AddScoped<ITenantConfigRepository, TenantConfigRepository>();
        services.AddScoped<ILandingHeroRepository, LandingHeroRepository>();
        services.AddScoped<ILandingFeaturesRepository, LandingFeaturesRepository>();
        services.AddScoped<ICompanyProjectRepository, CompanyProjectRepository>();
        services.AddScoped<IClientItemRepository, ClientItemRepository>();
        services.AddScoped<ITestimonialRepository, TestimonialRepository>();
        services.AddScoped<IServiceRepository, ServiceRepository>();
        services.AddScoped<ISocialLinkRepository, SocialLinkRepository>();
        services.AddScoped<IContactRepository, ContactRepository>();
        services.AddScoped<IBrandRepository, BrandRepository>();
        services.AddScoped<IStatsSectionRepository, StatsSectionRepository>();
        services.AddScoped<IFaqRepository, FaqRepository>();
        services.AddScoped<IPlanRepository, PlanRepository>();
        services.AddScoped<IGalleryItemRepository, GalleryItemRepository>();
        services.AddScoped<IDocumentRepository, DocumentRepository>();
        services.AddScoped<IAboutConfigRepository, AboutConfigRepository>();
        services.AddScoped<IProjectRepository, ProjectRepository>();
        services.AddScoped<IApiKeyRepository, ApiKeyRepository>();
        services.AddScoped<IProjectCorsRepository, ProjectCorsRepository>();


        services.AddScoped(typeof(IRepository<,>), typeof(BaseRepository<,>));

        services.AddSingleton<TimeProvider>(TimeProvider.System);
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddDatabaseDeveloperPageExceptionFilter();

        services
        .AddDefaultIdentity<ApplicationUser>(options =>
        {
            options.User.RequireUniqueEmail = true;
            options.Password.RequiredLength = 8;
        })
        .AddRoles<ApplicationRole>()
        .AddRoleManager<RoleManager<ApplicationRole>>()
        .AddEntityFrameworkStores<GridBaseDbContext>();

        var redisConnection = configuration["RedisConnection"];
        services.AddSingleton<IConnectionMultiplexer>(
            ConnectionMultiplexer.Connect(redisConnection!)
        );

        services.AddScoped<IAccountService, IdentityAccountService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IIdentityService, IdentityService>();
        services.AddScoped<IPresenceService, RedisPresenceService>();

        services.AddSignalR();
        services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<IdentitySeeder>();

        services.AddAuthorization();

        services.Configure<EmailSettings>(configuration.GetSection(EmailSettings.SectionName));
        services.AddScoped<IEmailService, EmailService>();

        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = configuration[ConnectionSettings.REDİS_CONNECTION];
            options.InstanceName = ConnectionSettings.REDİS_INSTANCE_NAME;
        });
        services.AddScoped<IAppCache, RedisCache>();
        services.AddMemoryCache();

        services.AddSingleton<IAppLogger, LogManager>();

        return services;
    }
}