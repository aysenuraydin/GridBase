using gridbase.Domain.Entities;
using gridbase.Domain.Repositories;
using gridbase.Infrastructure.Persistence.Common.Repositories;

namespace gridbase.Infrastructure.Persistence.Repositories;

public class MenuItemRepository : BaseRepository<MenuItem, long>, IMenuItemRepository
{
    public MenuItemRepository(GridBaseDbContext context) : base(context)
    {

    }
}