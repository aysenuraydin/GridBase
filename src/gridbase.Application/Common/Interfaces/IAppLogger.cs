using Serilog;

namespace gridbase.Application.Common.Interfaces;

public interface IAppLogger
{
    ILogger CreateMongoLogger();
    ILogger CreatePerformanceLogger();
}
