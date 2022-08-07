using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using PcStatsReporter.AspNetCore.Messages;
using PcStatsReporter.AspNetCore.SignalR;
using PcStatsReporter.AspNetCore.SignalR.Contracts;
using PcStatsReporter.Core.Maps;
using PcStatsReporter.Core.Models;
using Rebus.Bus;
using Rebus.Handlers;

namespace PcStatsReporter.AspNetCore.Handlers;

public class CpuSampleArrivedHandler : IHandleMessages<CpuSampleArrivedEvent>
{
    private readonly ILogger<CpuSampleArrivedHandler> _logger;
    private readonly IHubContext<ReporterHub> _hub;

    private readonly IBus _bus;
    private readonly IMap<CpuSample, CpuSampleDto> _map;

    public CpuSampleArrivedHandler(ILogger<CpuSampleArrivedHandler> logger, IHubContext<ReporterHub> hub, IMap<CpuSample, CpuSampleDto> map)
    {
        _logger = logger;
        _hub = hub;
        _map = map;
    }
    
    public async Task Handle(CpuSampleArrivedEvent message)
    {
        var dto = _map.Map(message.CpuSample);


        await _hub.Clients.All.SendAsync("handleCpuData", dto);
    }
}