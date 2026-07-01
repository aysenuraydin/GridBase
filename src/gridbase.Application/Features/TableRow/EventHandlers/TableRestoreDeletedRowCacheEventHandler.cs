using MediatR;
using gridbase.Application.Common.Constants;
using gridbase.Application.Common.Interfaces;
using gridbase.Domain.Events;

namespace gridbase.Application.Features.TableRows.EventHandlers;

public class TableRestoreDeletedRowCacheEventHandler : INotificationHandler<TableRowRestoredDeletedEvent>
{
    private readonly IAppCache _redisCache;
    public TableRestoreDeletedRowCacheEventHandler(IAppCache redisCache)
    {
        _redisCache = redisCache;
    }


    public async Task Handle(TableRowRestoredDeletedEvent notification, CancellationToken cancellationToken)
    {
        await _redisCache.RemoveCache($"{CacheConstants.GetDeletedTableRowsByTableId}-{notification.row?.TableId}");

        await _redisCache.RemoveCache($"{CacheConstants.GetTableRowsByTableId}-{notification.row?.TableId}");
    }
}