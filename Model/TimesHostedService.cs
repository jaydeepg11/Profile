using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

public class TimesHostedService : IHostedService, IDisposable
{
    private readonly ILogger<TimesHostedService> _logger;
    private Timer _timer;

    public TimesHostedService(ILogger<TimesHostedService> logger)
    {
        _logger = logger;
    }
    public void Dispose()
    {
        _timer?.Dispose();
    }

    //1.IHostedservice : to run service in backgroud package required :- dotnet add package Microsoft.Extensions.Hosting
    public Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Timed Hosted Service running.");

        _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));

        return Task.CompletedTask;
    }
    private void DoWork(object state)
    {
        _logger.LogInformation("Timed Hosted Service is working.");
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Timed Hosted Service is stopping.");

        _timer?.Change(Timeout.Infinite, 0);

        return Task.CompletedTask;
    }
}