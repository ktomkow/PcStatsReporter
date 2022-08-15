using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PcStatsReporter.Core.Maps;
using PcStatsReporter.Core.Models;
using PcStatsReporter.Grpc.Proto;

namespace PcStatsReporter.Client.CollectorServices;

public class RamCollectorService : BackgroundService
{
    private readonly AppContext _appContext;
    private readonly ILogger<RamCollectorService> _logger;
    private readonly ICollector<RamSample> _collector;
    private readonly IMap<RamSample, CollectedData> _map;
    private Collector.CollectorClient _client;

    public RamCollectorService(AppContext appContext, ILogger<RamCollectorService> logger, ICollector<RamSample> collector, IMap<RamSample, CollectedData> map)
    {
        _appContext = appContext;
        _logger = logger;
        _collector = collector;
        _map = map;
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
            try
            {
                RamSample ramSample = _collector.Collect();
                var mappedSample = _map.Map(ramSample);
                await _client.CollectAsync(mappedSample);
                
                _logger.LogDebug("{Sample} collected", nameof(RamSample));
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