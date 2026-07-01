
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using gridbase.Infrastructure.Identity;
using gridbase.Infrastructure.Persistence.Common;
using gridbase.Infrastructure.Persistence.Seeders;

namespace gridbase.Infrastructure.Persistence;

public static class DbInitExtensions
{
    public static async Task InitializeDb(this IApplicationBuilder app)
    {
        var scope = app.ApplicationServices.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<GridBaseDbContext>();

        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

        await new IdentitySeeder(userManager, roleManager).Seed(context);
        await new MenuItemSeeder().Seed(context);

    }

    private static async Task ApplyAllSeederFromAssembly(GridBaseDbContext context)
    {
        var seederType = typeof(ISeeder);
        var seeders = Assembly.GetExecutingAssembly().GetTypes()
            .Where(s => seederType.IsAssignableFrom(s)
                        && s != seederType
                        && !s.IsAbstract
                        && !s.IsInterface
                        && s.GetConstructor(Type.EmptyTypes) != null)
            .ToList();

        foreach (var type in seeders)
        {
            try
            {
                var seeder = Activator.CreateInstance(type) as ISeeder;
                if (seeder != null)
                    await seeder.Seed(context);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Seeder çalıştırılamadı: {type.Name} ➜ {ex.Message}");
            }
        }
    }
}
