using MediatR;
using gridbase.Application.Common.Constants;
using gridbase.Application.Common.Interfaces;
using gridbase.Domain.Events;

namespace gridbase.Application.Features.TableColumns.EventHandlers;

public class TableUpdateTableColumnWithModalCacheEventHandler : INotificationHandler<TableColumnWithModalUpdatedEvent>
{
    private readonly IAppCache _redisCache;
    public TableUpdateTableColumnWithModalCacheEventHandler(IAppCache redisCache)
    {
        _redisCache = redisCache;
    }
    public async Task Handle(TableColumnWithModalUpdatedEvent notification, CancellationToken cancellationToken)
    {
        await _redisCache.RemoveCache($"{CacheConstants.GetTableColumnsByTableId}-{notification.column.TableId}");
    }
}