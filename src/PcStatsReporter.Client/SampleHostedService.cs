using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PcStatsReporter.Core.Models;

namespace PcStatsReporter.Client;

public class SampleHostedService : IHostedService
{
    private readonly AppContext _appContext;
    private readonly ILogger<SampleHostedService> _logger;

    public SampleHostedService(AppContext appContext, ILogger<SampleHostedService> logger)
    {
        _appContext = appContext;
        _logger = logger;
    }
    
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogCritical("Start SampleHostedService");
        _logger.LogCritical("SampleHostedService Waiting..");
        await _appContext.WaitForInitialization();
        _logger.LogCritical("SampleHostedService Waiting DONE");

        var ram = new RamSample()
        {
            Id = Guid.NewGuid(),
            RegisteredAt = DateTime.UtcNow.AddSeconds(-3),
            InUse = 4.2
        };

        // Console.WriteLine(ram.ToString());

        var cpu = new CpuSample()
        {
            Id = Guid.NewGuid(),
            RegisteredAt = DateTime.UtcNow.AddSeconds(-3),
            Temperature = 64,
            AverageLoad = 49,
            Cores = new List<CoreSample>()
            {
                new CoreSample()
                {
                    CoreNumber = 1,
                    Speed = 2949,
                    Temperature = 67,
                    ThreadsLoad = new (uint threadNumber, uint threadLoad)[]
                    {
                        new(1, 23),
                        new(2, 21),
                    }
                },
                new CoreSample()
                {
                    CoreNumber = 2,
                    Speed = 2999,
                    Temperature = 62,
                    ThreadsLoad = new (uint threadNumber, uint threadLoad)[]
                    {
                        new(1, 31),
                        new(2, 36),
                    }
                }
            }
        };

        // Console.WriteLine(cpu);

        await Task.CompletedTask;
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
    }
}