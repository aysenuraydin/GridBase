using MediatR;
using gridbase.Application.Common.Constants;
using gridbase.Application.Common.Interfaces;
using gridbase.Domain.Events;

namespace gridbase.Application.Features.TableColumns.EventHandlers;

public class TableDeleteColumnCacheEventHandler : INotificationHandler<TableColumnDeletedEvent>
{
    private readonly IAppCache _redisCache;
    public TableDeleteColumnCacheEventHandler(IAppCache redisCache)
    {
        _redisCache = redisCache;
    }


    public async Task Handle(TableColumnDeletedEvent notification, CancellationToken cancellationToken)
    {
        await _redisCache.RemoveCache($"{CacheConstants.GetTableColumnsByTableId}-{notification.column.TableId}");
        await _redisCache.RemoveCache($"{CacheConstants.GetDeletedTableColumnsByTableId}-{notification.column.TableId}");
        await _redisCache.RemoveCache(CacheConstants.GetAllTableColumns);
    }
}