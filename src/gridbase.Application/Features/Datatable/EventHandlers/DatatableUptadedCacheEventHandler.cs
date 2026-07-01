using MediatR;
using gridbase.Application.Common.Constants;
using gridbase.Application.Common.Interfaces;
using gridbase.Domain.Events.DatatableEvents;

namespace gridbase.Application.Features.Datatables.EventHandlers;

public class DatatableUpdatedCacheEventHandler : INotificationHandler<DatatableUpdatedEvent>
{
    private readonly IAppCache _redisCache;
    public DatatableUpdatedCacheEventHandler(IAppCache redisCache)
    {
        _redisCache = redisCache;
    }

    public async Task Handle(DatatableUpdatedEvent notification, CancellationToken cancellationToken)
    {
        await _redisCache.RemoveCache(CacheConstants.GetDatatables);
        await _redisCache.RemoveCache($"{CacheConstants.GetDatatableById}-{notification.table.Id}");
        await _redisCache.RemoveCache(CacheConstants.GetDatatablesWithRelationships);
    }
}