using gridbase.Application.Common.Services;
using gridbase.Application.Services.Interfaces;
using gridbase.Domain.Common;
using gridbase.Domain.Entities;

namespace gridbase.Application.Services;

public class TableRowService : BaseService<TableRow>, ITableRowService
{
    public TableRowService(IRepository<TableRow, long> repository) : base(repository)
    {
    }
}