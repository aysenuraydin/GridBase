
using gridbase.Domain.Entities;
using gridbase.Domain.Repositories;

namespace gridbase.Domain.Common;

public interface IUnitOfWork : IDisposable
{
    ITableRepository TableRepository { get; }
    ITableColumnRepository TableColumnRepository { get; }
    ITableCellRepository TableCellRepository { get; }
    ITableRowRepository TableRowRepository { get; }
    IMenuItemRepository MenuItemRepository { get; }
    IBadgeRepository BadgeRepository { get; }
    IForeignTableRepository ForeignTableRepository { get; }


    IColumnUIRepository ColumnUIRepository { get; }
    IColumnDataRepository ColumnDataRepository { get; }
    IValidationRepository ValidationRepository { get; }
    IRulesRepository RulesRepository { get; }
    IProjectRepository ProjectRepository { get; }
    IGridBaseRepository GridBaseRepository { get; }

    int Commit();
    Task<int> CommitAsync(CancellationToken cancellationToken = default);

    Task<ITransaction> BeginTransactionAsync();

}