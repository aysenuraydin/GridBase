using gridbase.Domain.Entities;
using gridbase.Domain.Repositories;
using gridbase.Infrastructure.Persistence.Common.Repositories;

namespace gridbase.Infrastructure.Persistence.Repositories;

public class BadgeRepository : BaseRepository<Badge, long>, IBadgeRepository
{
    public BadgeRepository(GridBaseDbContext context) : base(context)
    {

    }
}