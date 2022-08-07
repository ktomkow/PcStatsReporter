using Microsoft.Extensions.Hosting;

namespace PcStatsReporter.Client.CollectorServices;

public class RamCollectorService : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            // to stuff
            
            await Task.Delay(TimeSpan.FromSeconds(1), stoppingToken);
        }
    }
}