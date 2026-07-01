using MediatR;
using gridbase.Application.Common.Constants;
using gridbase.Application.Common.Interfaces;
using gridbase.Domain.Events;

namespace gridbase.Application.Features.TableCells.EventHandlers;

public class UpdateTableCellCacheEventHandler : INotificationHandler<TableCellUpdatedEvent>
{
    private readonly IAppCache _redisCache;
    public UpdateTableCellCacheEventHandler(IAppCache redisCache)
    {
        _redisCache = redisCache;
    }


    public async Task Handle(TableCellUpdatedEvent notification, CancellationToken cancellationToken)
    {
        await _redisCache.RemoveCache($"{CacheConstants.GetTableRowsByTableId}-{notification.cell?.RowFk?.TableId}");
        await _redisCache.RemoveCache($"{CacheConstants.GetTableRowById}-{notification.cell?.RowId}");
    }
}