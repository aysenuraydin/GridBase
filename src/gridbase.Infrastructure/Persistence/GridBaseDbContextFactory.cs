// gridbase.Infrastructure/Persistence/GridBaseDbContextFactory.cs
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using gridbase.Infrastructure.Constants;

namespace gridbase.Infrastructure.Persistence;

public class GridBaseDbContextFactory : IDesignTimeDbContextFactory<GridBaseDbContext>
{
    public GridBaseDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../gridbase.WebApi"))
            .AddJsonFile("appsettings.json")
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<GridBaseDbContext>();
        optionsBuilder.UseSqlite(
            configuration.GetConnectionString(ConnectionSettings.DB_CONNECTION_KEY)
        );

        return new GridBaseDbContext(optionsBuilder.Options);
    }
}