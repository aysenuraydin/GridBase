using MediatR;
using Microsoft.Extensions.Logging;
using gridbase.Application.Common.Constants;
using gridbase.Application.Common.Interfaces;
using gridbase.Domain.Events;

namespace gridbase.Application.Features.MenuItems.EventHandlers;

public class MenuItemDeletedCacheEventHandler : INotificationHandler<MenuItemDeletedEvent>
{
    private readonly IAppCache _redisCache;
    public MenuItemDeletedCacheEventHandler(IAppCache redisCache)
    {
        _redisCache = redisCache;
    }

    public async Task Handle(MenuItemDeletedEvent notification, CancellationToken cancellationToken)
    {
        await _redisCache.RemoveCache(CacheConstants.GetMenuItems);
        await _redisCache.RemoveCache(CacheConstants.GetDeletedMenuItems);
        await _redisCache.RemoveCache($"{CacheConstants.GetMenuItemById}-{notification.item.Id}");
    }
}