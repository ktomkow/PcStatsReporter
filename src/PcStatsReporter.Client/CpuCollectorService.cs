using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PcStatsReporter.Client.Messages;
using PcStatsReporter.Core.Models;
using PcStatsReporter.Grpc.Proto;
using Rebus.Handlers;

namespace PcStatsReporter.Client;

public class CpuCollectorService : BackgroundService, IHandleMessages<SettingsChanged>
{
    private readonly AppContext _appContext;
    private readonly ILogger<CpuCollectorService> _logger;
    private readonly ICollector<CpuSample> _collector;
    private Collector.CollectorClient _client;
    private CancellationToken _stoppingToken;

    public CpuCollectorService(AppContext appContext, ILogger<CpuCollectorService> logger, ICollector<CpuSample> collector)
    {
        _appContext = appContext;
        _logger = logger;
        _collector = collector;
    }


    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _stoppingToken = stoppingToken;
        await this.StartAsync();
    }

    private async Task StartAsync()
    {
        _logger.LogInformation("Starting {Service}", this.GetType().Name);
        await _appContext.WaitForInitialization();
        
        _logger.LogInformation("Started {Service}", this.GetType().Name);

        var result = _collector.Collect();
        
        _logger.LogInformation("GOT FIRST RESULT");
        _logger.LogInformation($"{result}");
    }

    private async Task StopAsync()
    {
        _logger.LogInformation("Stopping {Service}", this.GetType().Name);
        await Task.CompletedTask;
    }

    public async Task Handle(SettingsChanged message)
    {
        await this.StopAsync();
        await this.StartAsync();
    }
}