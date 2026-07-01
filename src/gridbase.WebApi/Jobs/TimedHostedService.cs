namespace gridbase.WebApi.Jobs;

public class TimedHostedService : IHostedService, IDisposable
//1 IHostedService kalıtım aldık
//1 IDisposable kalıtım aldı işlemler tamamladığın bellekten otomatik olarak silinmesi için IDisposable edip implemente ettik
{ //implemente edincestart ve stop diye iki metod oluştu

    private int executionCount = 0;
    private readonly ILogger<TimedHostedService> _logger;
    private Timer? _timer = null; //

    public TimedHostedService(ILogger<TimedHostedService> logger)
    {
        _logger = logger;
    }

    public Task StartAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Timed Hosted Service running.");

        _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));

        //4. paremetreyi daha esnek şekilde belirtmek istersek bak => cron expression - https://crontab.cronhub.io/
        return Task.CompletedTask;
    }

    private void DoWork(object? state)
    {
        //! countu glabal değişken gibi tanımladık böylece hers seferinde 0 a değil bir önceki executionCount değerine 1 ekleyecek
        var count = Interlocked.Increment(ref executionCount);//DoWork her çalıştığında countun değeri 1 arttırdık.

        _logger.LogInformation(
            "Timed Hosted Service is working. Count: {Count}", count);
        //burada başka işlerde yapabilirdik şimdilik bötle bıraktım
    }
    /*
    private void DoWork(object? state)
    {
        //*Belirli şartlarda alışmasnı isteseydik.

        if (DateTime.Now.DayOfWeek == DayOfWeek.Saturday &&
        DateTime.Now.Hour == 10 && DateTime.Now.Minute == 0 &&
        executionCount == 0)
        //*pazatesi günü saat 10.00 da sadece 1 kere çalışmasını istiyorsak
        {
            var count = Interlocked.Increment(ref executionCount);

            _logger.LogInformation(
                "Timed Hosted Service is working. Count: {Count}", count);
        }
    }
    */

    public Task StopAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Timed Hosted Service is stopping.");

        _timer?.Change(Timeout.Infinite, 0);

        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }

}