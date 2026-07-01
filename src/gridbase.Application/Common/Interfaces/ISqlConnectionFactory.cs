using System.Data;

namespace gridbase.Application.Common.Interfaces
{
    public interface ISqlConnectionFactory
    {
        IDbConnection CreateConnection();
    }
}