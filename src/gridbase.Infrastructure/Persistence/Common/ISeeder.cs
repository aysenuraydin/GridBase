using gridbase.Application.Common.Interfaces;

namespace gridbase.Infrastructure.Persistence.Common;

public interface ISeeder
{
    Task Seed(IGridBaseDbContext context);
}