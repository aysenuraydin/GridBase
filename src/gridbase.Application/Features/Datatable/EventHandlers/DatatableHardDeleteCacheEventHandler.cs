using MediatR;
using Microsoft.Extensions.Logging;
using gridbase.Application.Common.Constants;
using gridbase.Application.Common.Interfaces;
using gridbase.Domain.Events;
using gridbase.Domain.Events.DatatableEvents;

namespace gridbase.Application.Features.Datatables.EventHandlers;

public class DatatableHardDeleteCacheEventHandler : INotificationHandler<DatatableHardDeletedEvent>
{
    private readonly IAppCache _redisCache;
    private readonly ILogger<DatatableHardDeleteCacheEventHandler> _logger;
    public DatatableHardDeleteCacheEventHandler(IAppCache redisCache, ILogger<DatatableHardDeleteCacheEventHandler> logger)
    {
        _redisCache = redisCache;
        _logger = logger;
    }

    public async Task Handle(DatatableHardDeletedEvent notification, CancellationToken cancellationToken)
    {
        await _redisCache.RemoveCache(CacheConstants.GetDeletedDatatables);
    }
}