using gridbase.Domain.Entities;
using gridbase.Domain.Repositories;
using gridbase.Infrastructure.Persistence.Common.Repositories;

namespace gridbase.Infrastructure.Persistence.Repositories;

public class ForeignTableRepository : BaseRepository<ForeignTable, long>, IForeignTableRepository
{
    public ForeignTableRepository(GridBaseDbContext context) : base(context)
    {

    }
}