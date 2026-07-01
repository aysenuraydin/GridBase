using gridbase.Domain.Entities;
using gridbase.Domain.Repositories;
using gridbase.Infrastructure.Persistence.Common.Repositories;

namespace gridbase.Infrastructure.Persistence.Repositories;

public class ColumnUIRepository : BaseRepository<ColumnUIConfig, long>, IColumnUIRepository
{
    public ColumnUIRepository(GridBaseDbContext context) : base(context)
    {

    }
}