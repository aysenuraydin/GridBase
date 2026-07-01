using MediatR;
using Microsoft.Extensions.Logging;
using gridbase.Application.Common.Constants;
using gridbase.Application.Common.Interfaces;
using gridbase.Domain.Events;

namespace gridbase.Application.Features.MenuItems.EventHandlers;

public class MenuItemHardDeletedCacheEventHandler : INotificationHandler<MenuItemHardDeletedEvent>
{
    private readonly IAppCache _redisCache;
    public MenuItemHardDeletedCacheEventHandler(IAppCache redisCache)
    {
        _redisCache = redisCache;
    }

    public async Task Handle(MenuItemHardDeletedEvent notification, CancellationToken cancellationToken)
    {
        await _redisCache.RemoveCache(CacheConstants.GetDeletedMenuItems);
        await _redisCache.RemoveCache($"{CacheConstants.GetMenuItemById}-{notification.item.Id}");
    }
}