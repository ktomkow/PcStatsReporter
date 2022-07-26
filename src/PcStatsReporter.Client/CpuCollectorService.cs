using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PcStatsReporter.Client.Messages;
using PcStatsReporter.Core.Models;
using PcStatsReporter.Grpc.Proto;
using Rebus.Handlers;

namespace PcStatsReporter.Client;

public class CpuCollectorService : BackgroundService
{
    private readonly AppContext _appContext;
    private readonly ILogger<CpuCollectorService> _logger;
    private readonly ICollector<CpuSample> _collector;
    private Collector.CollectorClient _client;
    private CancellationToken _stoppingToken;
    private Task _workingTask;

    public CpuCollectorService(AppContext appContext, ILogger<CpuCollectorService> logger, ICollector<CpuSample> collector)
    {
        _appContext = appContext;
        _logger = logger;
        _collector = collector;
    }


    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _stoppingToken = stoppingToken;
        await StartAsync();
    }

    private async Task StartAsync()
    {
        _logger.LogInformation("Starting {Service}", this.GetType().Name);
        await _appContext.WaitForInitialization();
        _logger.LogInformation("Started {Service}", this.GetType().Name);

        _workingTask = Task.Run(async () => await Work(), _stoppingToken);
    }

    private async Task Work()
    {
        while (true)
        {
            try
            {
                CpuSample? cpuSample = _collector.Collect();
                
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