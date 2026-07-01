using MediatR;
using gridbase.Application.Common.Constants;
using gridbase.Application.Common.Interfaces;
using gridbase.Domain.Events;

namespace gridbase.Application.Features.TableColumns.EventHandlers;

public class TableUpdateTableColumnWithFunctionCacheEventHandler : INotificationHandler<TableColumnWithFunctionUpdatedEvent>
{
    private readonly IAppCache _redisCache;
    public TableUpdateTableColumnWithFunctionCacheEventHandler(IAppCache redisCache)
    {
        _redisCache = redisCache;
    }
    public async Task Handle(TableColumnWithFunctionUpdatedEvent notification, CancellationToken cancellationToken)
    {
        await _redisCache.RemoveCache($"{CacheConstants.GetTableColumnsByTableId}-{notification.column.TableId}");
    }
}