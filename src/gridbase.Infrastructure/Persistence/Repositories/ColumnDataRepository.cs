using gridbase.Domain.Entities;
using gridbase.Domain.Repositories;
using gridbase.Infrastructure.Persistence.Common.Repositories;

namespace gridbase.Infrastructure.Persistence.Repositories;

public class ColumnDataRepository : BaseRepository<ColumnDataConfig, long>, IColumnDataRepository
{
    public ColumnDataRepository(GridBaseDbContext context) : base(context)
    {

    }
}