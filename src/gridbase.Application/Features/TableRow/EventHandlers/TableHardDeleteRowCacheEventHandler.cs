using MediatR;
using gridbase.Application.Common.Constants;
using gridbase.Application.Common.Interfaces;
using gridbase.Domain.Events;

namespace gridbase.Application.Features.TableRows.EventHandlers;

public class TableHardDeleteRowCacheEventHandler : INotificationHandler<TableRowHardDeletedEvent>
{
    private readonly IAppCache _redisCache;
    public TableHardDeleteRowCacheEventHandler(IAppCache redisCache)
    {
        _redisCache = redisCache;
    }

    public async Task Handle(TableRowHardDeletedEvent notification, CancellationToken cancellationToken)
    {
        await _redisCache.RemoveCache($"{CacheConstants.GetDeletedTableRowsByTableId}-{notification.row.TableId}");
    }
}