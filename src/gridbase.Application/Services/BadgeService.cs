using gridbase.Application.Common.Services;
using gridbase.Application.Services.Interfaces;
using gridbase.Domain.Common;
using gridbase.Domain.Entities;

namespace gridbase.Application.Services;

public class BadgeService : BaseService<Badge>, IBadgeService
{
    public BadgeService(IRepository<Badge, long> repository) : base(repository)
    {
    }
}