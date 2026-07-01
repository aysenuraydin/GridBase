using MediatR;
using gridbase.Application.Common.Constants;
using gridbase.Application.Common.Interfaces;
using gridbase.Domain.Events;

namespace gridbase.Application.Features.TableColumns.EventHandlers;

public class TableHardDeleteColumnCacheEventHandler : INotificationHandler<TableColumnHardDeletedEvent>
{
    private readonly IAppCache _redisCache;
    public TableHardDeleteColumnCacheEventHandler(IAppCache redisCache)
    {
        _redisCache = redisCache;
    }


    public async Task Handle(TableColumnHardDeletedEvent notification, CancellationToken cancellationToken)
    {
        await _redisCache.RemoveCache($"{CacheConstants.GetDeletedTableColumnsByTableId}-{notification.column.TableId}");
    }
}