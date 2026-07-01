using MediatR;
using Microsoft.Extensions.Logging;
using gridbase.Application.Common.Constants;
using gridbase.Application.Common.Interfaces;
using gridbase.Domain.Events;
using gridbase.Domain.Events.DatatableEvents;

namespace gridbase.Application.Features.Datatables.EventHandlers;

public class DatatableRestoreDeletedCacheEventHandler : INotificationHandler<DatatableRestoreDeletedEvent>
{
    private readonly IAppCache _redisCache;
    public DatatableRestoreDeletedCacheEventHandler(IAppCache redisCache)
    {
        _redisCache = redisCache;
    }

    public async Task Handle(DatatableRestoreDeletedEvent notification, CancellationToken cancellationToken)
    {
        await _redisCache.RemoveCache(CacheConstants.GetDatatables);
        await _redisCache.RemoveCache(CacheConstants.GetDeletedDatatables);
        await _redisCache.RemoveCache(CacheConstants.GetDatatablesWithRelationships);

        await _redisCache.RemoveCache(CacheConstants.GetAllTableColumns);
        await _redisCache.RemoveCache($"{CacheConstants.GetTableColumnsByTableId}-{notification.table.Id}");
        await _redisCache.RemoveCache($"{CacheConstants.GetTableRowsByTableId}-{notification.table.Id}");
    }
}