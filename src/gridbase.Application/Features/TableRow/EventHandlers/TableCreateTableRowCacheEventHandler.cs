using MediatR;
using gridbase.Application.Common.Constants;
using gridbase.Application.Common.Interfaces;
using gridbase.Domain.Events;

namespace gridbase.Application.Features.TableRows.EventHandlers;

public class TableCreateTableRowCacheEventHandler : INotificationHandler<TableRowCreatedEvent>
{
    private readonly IAppCache _redisCache;
    public TableCreateTableRowCacheEventHandler(IAppCache redisCache)
    {
        _redisCache = redisCache;
    }


    public async Task Handle(TableRowCreatedEvent notification, CancellationToken cancellationToken)
    {
        // Cache temizleme mantığı burada
        await _redisCache.RemoveCache($"{CacheConstants.GetTableRowsByTableId}-{notification.row.TableId}");
    }
}