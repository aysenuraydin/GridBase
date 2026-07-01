using MediatR;
using Microsoft.Extensions.Logging;
using gridbase.Application.Common.Constants;
using gridbase.Application.Common.Interfaces;
using gridbase.Domain.Events;

namespace gridbase.Application.Features.MenuItems.EventHandlers;

public class MenuItemCreatedCacheEventHandler : INotificationHandler<MenuItemCreatedEvent>
{
    private readonly IAppCache _redisCache;
    public MenuItemCreatedCacheEventHandler(IAppCache redisCache)
    {
        _redisCache = redisCache;
    }

    public async Task Handle(MenuItemCreatedEvent notification, CancellationToken cancellationToken)
    {
        await _redisCache.RemoveCache(CacheConstants.GetMenuItems);
    }
}