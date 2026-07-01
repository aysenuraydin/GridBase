using MediatR;
using Newtonsoft.Json;
using FluentScheduler;
using gridbase.Infrastructure.Persistence;

namespace gridbase.WebApi.Jobs;


public class ProcessOutboxMessagesJob : IJob
{
    private readonly IServiceProvider _serviceProvider;

    public ProcessOutboxMessagesJob(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public void Execute()
    {
        // 🔒 Hidden. Akış: scope aç → işlenmemiş ilk N mesajı al →
        //   her birini JSON'dan event'e çöz → MediatR ile publish →
        //   işlendi olarak işaretle (hata olursa kaydet) → kaydet.
        throw new NotImplementedException("Source available on request.");
    }
}