using Microsoft.EntityFrameworkCore;
using gridbase.Domain.Entities;
using gridbase.Domain.Repositories;
using gridbase.Infrastructure.Persistence.Common.Repositories;

namespace gridbase.Infrastructure.Persistence.Repositories;

public class ValidationRepository : BaseRepository<ColumnValidationConfig, long>, IValidationRepository
{
    public ValidationRepository(GridBaseDbContext context) : base(context)
    {

    }
}