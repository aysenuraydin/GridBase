using MediatR;
using gridbase.Application.Common.Constants;
using gridbase.Application.Common.Interfaces;
using gridbase.Domain.Events;

namespace gridbase.Application.Features.TableColumns.EventHandlers;

public class TableUpdateTableColumnWithOptionCacheEventHandler : INotificationHandler<TableColumnWithOptionUpdatedEvent>
{
    private readonly IAppCache _redisCache;
    public TableUpdateTableColumnWithOptionCacheEventHandler(IAppCache redisCache)
    {
        _redisCache = redisCache;
    }
    public async Task Handle(TableColumnWithOptionUpdatedEvent notification, CancellationToken cancellationToken)
    {
        await _redisCache.RemoveCache($"{CacheConstants.GetTableColumnsByTableId}-{notification.column.TableId}");
    }
}