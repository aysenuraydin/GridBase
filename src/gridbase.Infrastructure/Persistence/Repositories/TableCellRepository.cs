using gridbase.Domain.Entities;
using gridbase.Domain.Repositories;
using gridbase.Infrastructure.Persistence.Common.Repositories;

namespace gridbase.Infrastructure.Persistence.Repositories;

public class TableCellRepository : BaseRepository<TableCell, long>, ITableCellRepository
{
    public TableCellRepository(GridBaseDbContext context) : base(context)
    {

    }
}