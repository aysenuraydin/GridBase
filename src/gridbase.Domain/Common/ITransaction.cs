
using gridbase.Domain.Repositories;

namespace gridbase.Domain.Common;

public interface ITransaction : IDisposable
{
    Task CommitAsync();
    Task RollbackAsync();

}