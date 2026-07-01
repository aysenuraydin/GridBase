using Microsoft.EntityFrameworkCore;
using gridbase.Domain.Entities;
using gridbase.Domain.Repositories;
using gridbase.Infrastructure.Persistence.Common.Repositories;

namespace gridbase.Infrastructure.Persistence.Repositories;

public class TableColumnRepository : BaseRepository<TableColumn, long>, ITableColumnRepository
{
    private readonly GridBaseDbContext _context;
    private readonly DbSet<TableColumn> _column;

    public TableColumnRepository(GridBaseDbContext context) : base(context)
    {
        _context = context;
        _column = _context.Set<TableColumn>();
    }

    public async Task<long> CreateTableColumn(TableColumn entity)
    {
        _column.Add(entity);
        await _context.SaveChangesAsync();

        return entity.Id;
    }
}