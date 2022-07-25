using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PcStatsReporter.Client.Messages;
using Rebus.Bus;

namespace PcStatsReporter.Client.Initialization;

public class InitService : BackgroundService
{
    private readonly IBus _bus;
    private readonly ILogger<InitService> _logger;

    public InitService(IBus bus, ILogger<InitService> logger)
    {
        _bus = bus;
        _logger = logger;
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await Task.Delay(TimeSpan.FromSeconds(1), stoppingToken);
        _logger.LogInformation("START INITIALIZING");
        await _bus.Publish(new InitializeCommand());
    }
}