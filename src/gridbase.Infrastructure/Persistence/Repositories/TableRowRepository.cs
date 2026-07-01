using gridbase.Domain.Entities;
using gridbase.Domain.Repositories;
using gridbase.Infrastructure.Persistence.Common.Repositories;

namespace gridbase.Infrastructure.Persistence.Repositories;

public class TableRowRepository : BaseRepository<TableRow, long>, ITableRowRepository
{
    public TableRowRepository(GridBaseDbContext context) : base(context)
    {

    }
}