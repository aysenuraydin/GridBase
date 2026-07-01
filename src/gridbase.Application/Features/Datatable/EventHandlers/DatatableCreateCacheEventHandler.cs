using MediatR;
using Microsoft.Extensions.Logging;
using gridbase.Application.Common.Constants;
using gridbase.Application.Common.Interfaces;
using gridbase.Domain.Events;
using gridbase.Domain.Events.DatatableEvents;

namespace gridbase.Application.Features.Datatables.EventHandlers;

public class DatatableCreateCacheEventHandler : INotificationHandler<DatatableCreatedEvent>
{
    private readonly IAppCache _redisCache;
    private readonly ILogger<DatatableCreateCacheEventHandler> _logger;
    public DatatableCreateCacheEventHandler(IAppCache redisCache, ILogger<DatatableCreateCacheEventHandler> logger)
    {
        _redisCache = redisCache;
        _logger = logger;
    }

    public async Task Handle(DatatableCreatedEvent notification, CancellationToken cancellationToken)
    {
        Console.WriteLine("DatatableCreatedEvent ÇALIŞTI. " + notification.table.Name + " İSİMLİ TABLO EKLENDİ.");
        _logger.LogInformation("DatatableCreatedEvent ÇALIŞTI. {TableName} İSİMLİ TABLO EKLENDİ.", notification.table.Name);

        await _redisCache.RemoveCache(CacheConstants.GetDatatables);
        await _redisCache.RemoveCache(CacheConstants.GetDatatablesWithRelationships);
    }
}