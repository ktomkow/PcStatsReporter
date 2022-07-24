using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PcStatsReporter.Client.Messages;
using PcStatsReporter.Grpc.Proto;
using Rebus.Handlers;

namespace PcStatsReporter.Client;

public class CpuCollector : BackgroundService, IHandleMessages<SettingsChanged>
{
    private readonly AppContext _appContext;
    private readonly ILogger<CpuCollector> _logger;
    private Collector.CollectorClient _client;
    private CancellationToken _stoppingToken;

    public CpuCollector(AppContext appContext, ILogger<CpuCollector> logger)
    {
        _appContext = appContext;
        _logger = logger;
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