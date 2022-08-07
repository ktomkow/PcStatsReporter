using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PcStatsReporter.Core.Models;
using PcStatsReporter.Grpc.Proto;

namespace PcStatsReporter.Client.CollectorServices;

public class RamCollectorService : BackgroundService
{
    private readonly AppContext _appContext;
    private readonly ILogger<RamCollectorService> _logger;
    private readonly ICollector<RamSample> _collector;
    private Collector.CollectorClient _client;

    public RamCollectorService(AppContext appContext, ILogger<RamCollectorService> logger, ICollector<RamSample> collector)
    {
        _appContext = appContext;
        _logger = logger;
        _collector = collector;
    }

    public override Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting {Service}", this.GetType().Name);
        return base.StartAsync(cancellationToken);
    }

    public override Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Stopping {Service}", this.GetType().Name);
        return base.StopAsync(cancellationToken);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await _appContext.WaitForInitialization();

        _client = new Collector.CollectorClient(_appContext.ClientChannel);
        
        while (!stoppingToken.IsCancellationRequested)
        {
            // to stuff

            try
            {
                RamSample? ramSample = _collector.Collect();
                // todo: map and send
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error during collecting RAM Sample");
            }
            finally
            {
                await Task.Delay(_appContext.Settings.RamCollectSettings.Period, stoppingToken);
            }
        }
    }
}