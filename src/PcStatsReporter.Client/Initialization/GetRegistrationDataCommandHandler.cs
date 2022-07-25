using Microsoft.Extensions.Logging;
using PcStatsReporter.Client.Messages;
using PcStatsReporter.Core.Models;
using PcStatsReporter.LibreHardware;
using Rebus.Bus;
using Rebus.Handlers;

namespace PcStatsReporter.Client.Initialization;

public class GetRegistrationDataCommandHandler : IHandleMessages<GetRegistrationDataCommand>
{
    private readonly ILogger<GetRegistrationDataCommandHandler> _logger;
    private readonly IBus _bus;
    private readonly PcInfoCollector _pcInfoCollector;

    public GetRegistrationDataCommandHandler(ILogger<GetRegistrationDataCommandHandler> logger, IBus bus, PcInfoCollector pcInfoCollector)
    {
        _logger = logger;
        _bus = bus;
        _pcInfoCollector = pcInfoCollector;
    }

    public async Task Handle(GetRegistrationDataCommand message)
    {
        var pcInfo = await CollectPcInfo();
        _logger.LogInformation("Got PcInfo: {@PcInfo}", pcInfo);

        var @event = new GotRegistrationDataEvent()
        {
            CpuName = pcInfo.CpuName,
            GpuName = pcInfo.GpuName,
            TotalRam = pcInfo.TotalRam
        };

        await _bus.Publish(@event);
    }
    
    private async Task<PcInfo> CollectPcInfo()
    {
        _logger.LogInformation("Collecting PcInfo");
        return await Task.Factory.StartNew(() =>
        {
            var result = _pcInfoCollector.Collect();
            return result;
        });
    }
}