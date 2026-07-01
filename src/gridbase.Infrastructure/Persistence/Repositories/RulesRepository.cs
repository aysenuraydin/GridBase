using Microsoft.EntityFrameworkCore;
using gridbase.Domain.Entities;
using gridbase.Domain.Repositories;
using gridbase.Infrastructure.Persistence.Common.Repositories;

namespace gridbase.Infrastructure.Persistence.Repositories;

public class RulesRepository : BaseRepository<RulesValidationConfig, long>, IRulesRepository
{
    public RulesRepository(GridBaseDbContext context) : base(context)
    {

    }
}