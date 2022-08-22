using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PcStatsReporter.Core.Messages;
using PcStatsReporter.Core.Models;
using Rebus.Bus;

namespace PcStatsReporter.AspNetCore.DummyClient;

public class DummyClientService : BackgroundService
{
    private readonly ILogger<DummyClientService> _logger;
    private readonly IBus _bus;
    private readonly ICollector<CpuSample> _cpuSampleCollector;
    private readonly ICollector<RamSample> _ramSampleCollector;
    private readonly ICollector<GpuSample> _gpuSampleCollector;
    private readonly ICollector<PcInfo> _pcInfoCollector;
    private readonly DummyClientSettings _settings;

    public DummyClientService(ILogger<DummyClientService> logger,
        IBus bus,
        ICollector<CpuSample> cpuSampleCollector,
        ICollector<RamSample> ramSampleCollector,
        ICollector<GpuSample> gpuSampleCollector,
        ICollector<PcInfo> pcInfoCollector,
        DummyClientSettings settings)
    {
        _logger = logger;
        _bus = bus;
        _cpuSampleCollector = cpuSampleCollector;
        _ramSampleCollector = ramSampleCollector;
        _gpuSampleCollector = gpuSampleCollector;
        _pcInfoCollector = pcInfoCollector;
        _settings = settings;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogWarning("{Service} enabled", nameof(DummyClientService));
        _logger.LogInformation("{Service} is waiting {Seconds} seconds", nameof(DummyClientService), _settings.HoldTime.TotalSeconds);
        await Task.Delay(_settings.HoldTime, stoppingToken);
        _logger.LogInformation("{Service} is starting", nameof(DummyClientService));

        try
        {
            await _bus.Publish(new ReportingClientRegisteredEvent()
            {
                PcInfo = _pcInfoCollector.Collect()
            });
            
            _logger.LogInformation("{Service} registered {Name}", nameof(DummyClientService), nameof(PcInfo));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error during dummy client registration");
            throw;
        }
        
        _logger.LogInformation("{Service} begins samples collecting with period {Seconds} seconds", nameof(DummyClientService), _settings.CollectPeriod.TotalSeconds);
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await _bus.Publish(new CpuSampleArrivedEvent() {CpuSample = _cpuSampleCollector.Collect()});
                await _bus.Publish(new GpuSampleArrivedEvent() {GpuSample = _gpuSampleCollector.Collect()});
                await _bus.Publish(new RamSampleArrivedEvent() {RamSample = _ramSampleCollector.Collect()});
                _logger.LogDebug("{Service} collected samples", nameof(DummyClientService));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error during dummy client samples collection");
            }
            finally
            {
                await Task.Delay(_settings.CollectPeriod, stoppingToken);
            }
        }
    }
}