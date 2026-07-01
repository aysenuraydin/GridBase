using gridbase.Application.Common.Services;
using gridbase.Application.Services.Interfaces;
using gridbase.Domain.Common;
using gridbase.Domain.Entities;

namespace gridbase.Application.Services;

public class TableColumnService : BaseService<TableColumn>, ITableColumnService
{
    public TableColumnService(IRepository<TableColumn, long> repository) : base(repository)
    {
    }
}