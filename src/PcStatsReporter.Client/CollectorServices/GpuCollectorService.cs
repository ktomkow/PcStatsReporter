using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PcStatsReporter.Core.Models;
using PcStatsReporter.Grpc.Proto;

namespace PcStatsReporter.Client.CollectorServices;

public class GpuCollectorService : BackgroundService
{
    private readonly AppContext _appContext;
    private readonly ILogger<GpuCollectorService> _logger;
    private readonly ICollector<GpuSample> _collector;
    
    private Collector.CollectorClient _client;
    private CancellationToken _stoppingToken;
    private Task _workingTask;
    
    public GpuCollectorService(AppContext appContext, ILogger<GpuCollectorService> logger, ICollector<GpuSample> collector)
    {
        _appContext = appContext;
        _logger = logger;
        _collector = collector;
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _stoppingToken = stoppingToken;
        await _appContext.WaitForInitialization();
        
        _client = new Collector.CollectorClient(_appContext.ClientChannel);
        
        await StartAsync();
    }
    
    private Task StartAsync()
    {
        _logger.LogInformation("Starting {Service}", this.GetType().Name);

        _workingTask = Task.Run(async () => await Work(), _stoppingToken);

        return _workingTask;
    }

    private async Task Work()
    {
        while (true)
        {
            try
            {
                GpuSample? gpuSample = _collector.Collect();
                // var mappedSample = _map.Map(gpuSample);
                // await _client.CollectAsync(mappedSample);
                Console.WriteLine(gpuSample);

            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error during collecting CPU Sample");
            }
            finally
            {
                await Task.Delay(_appContext.Settings.CpuCollectSettings.Period, _stoppingToken);
            }
        }
    }
}