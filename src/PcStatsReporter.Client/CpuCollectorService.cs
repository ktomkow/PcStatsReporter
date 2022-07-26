using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PcStatsReporter.Core.Maps;
using PcStatsReporter.Core.Models;
using PcStatsReporter.Grpc.Proto;

namespace PcStatsReporter.Client;

public class CpuCollectorService : BackgroundService
{
    private readonly AppContext _appContext;
    private readonly ILogger<CpuCollectorService> _logger;
    private readonly ICollector<CpuSample> _collector;
    private readonly IMap<CpuSample, CollectedData> _map;
    private Collector.CollectorClient _client;
    private CancellationToken _stoppingToken;
    private Task _workingTask;

    public CpuCollectorService(AppContext appContext, ILogger<CpuCollectorService> logger, ICollector<CpuSample> collector, IMap<CpuSample, CollectedData> map)
    {
        _appContext = appContext;
        _logger = logger;
        _collector = collector;
        _map = map;
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _stoppingToken = stoppingToken;
        await _appContext.WaitForInitialization();
        
        _client = new Collector.CollectorClient(_appContext.ClientChannel);
        
        StartAsync();
    }

    private void StartAsync()
    {
        _logger.LogInformation("Starting {Service}", this.GetType().Name);

        _workingTask = Task.Run(async () => await Work(), _stoppingToken);
    }

    private async Task Work()
    {
        while (true)
        {
            try
            {
                CpuSample? cpuSample = _collector.Collect();
                var mappedSample = _map.Map(cpuSample);
                await _client.CollectAsync(mappedSample);

            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error during collecting CPU Sample");
            }
            finally
            {
                await Task.Delay(_appContext.Settings.CpuCollectSettings.Period);
            }
        }

        await Task.CompletedTask;
    }
}