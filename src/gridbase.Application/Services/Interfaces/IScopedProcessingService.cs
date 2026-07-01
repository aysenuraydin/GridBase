namespace gridbase.Application.Services.Interfaces;

public interface IScopedProcessingService
{
    Task DoWork(CancellationToken stoppingToken);
}