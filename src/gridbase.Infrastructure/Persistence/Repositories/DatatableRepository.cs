using gridbase.Domain.Entities;
using gridbase.Domain.Repositories;
using gridbase.Infrastructure.Persistence.Common.Repositories;

namespace gridbase.Infrastructure.Persistence.Repositories;

public class TableRepository : BaseRepository<Datatable, long>, ITableRepository
{
    public TableRepository(GridBaseDbContext context) : base(context)
    {

    }
}