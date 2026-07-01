using gridbase.Domain.Common;
using gridbase.Domain.Entities;

namespace gridbase.Domain.Repositories;

public interface ITableColumnRepository : IRepository<TableColumn, long>
{
    Task<long> CreateTableColumn(TableColumn entity);
}