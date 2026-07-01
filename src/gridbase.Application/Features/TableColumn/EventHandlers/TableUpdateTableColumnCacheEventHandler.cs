using MediatR;
using gridbase.Application.Common.Constants;
using gridbase.Application.Common.Interfaces;
using gridbase.Domain.Events;

namespace gridbase.Application.Features.TableColumns.EventHandlers;

public class TableUpdateTableColumnCacheEventHandler : INotificationHandler<TableColumnUpdatedEvent>
{
    private readonly IAppCache _redisCache;
    public TableUpdateTableColumnCacheEventHandler(IAppCache redisCache)
    {
        _redisCache = redisCache;
    }
    public async Task Handle(TableColumnUpdatedEvent notification, CancellationToken cancellationToken)
    {
        await _redisCache.RemoveCache(CacheConstants.GetAllTableColumns);
        await _redisCache.RemoveCache($"{CacheConstants.GetTableColumnsByTableId}-{notification.column.TableId}");
    }
}