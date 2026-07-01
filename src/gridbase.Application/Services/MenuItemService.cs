using gridbase.Application.Common.Services;
using gridbase.Application.Services.Interfaces;
using gridbase.Domain.Common;
using gridbase.Domain.Entities;

namespace gridbase.Application.Services;

public class MenuItemService : BaseService<MenuItem>, IMenuItemService
{
    public MenuItemService(IRepository<MenuItem, long> repository) : base(repository)
    {
    }
}