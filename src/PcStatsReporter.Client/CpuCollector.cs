using Microsoft.Extensions.Hosting;
using PcStatsReporter.Grpc.Proto;

namespace PcStatsReporter.Client;

public class CpuCollector : BackgroundService
{
    private readonly AppContext _appContext;
    private Collector.CollectorClient _client;

    public CpuCollector(AppContext appContext)
    {
        _appContext = appContext;
        
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // await _appContext.Initialize();
        // _client = new Collector.CollectorClient(_appContext.GrpcChannel);
    }
}