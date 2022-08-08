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

public class SampleArrivedCallClientsHandler : IHandleMessages<CpuSampleArrivedEvent>, IHandleMessages<GpuSampleArrivedEvent>, IHandleMessages<RamSampleArrivedEvent>
{
    private readonly ILogger<SampleArrivedCallClientsHandler> _logger;
    private readonly IHubContext<ReporterHub> _hub;

    private readonly IBus _bus;
    private readonly IMap<CpuSample, CpuSampleDto> _cpuMap;
    private readonly IMap<GpuSample, GpuSampleDto> _gpuMap;
    private readonly IMap<RamSample, RamSampleDto> _ramMap;

    public SampleArrivedCallClientsHandler(ILogger<SampleArrivedCallClientsHandler> logger,
        IHubContext<ReporterHub> hub,
        IBus bus,
        IMap<CpuSample, CpuSampleDto> cpuMap,
        IMap<GpuSample, GpuSampleDto> gpuMap,
        IMap<RamSample, RamSampleDto> ramMap)
    {
        _logger = logger;
        _hub = hub;
        _bus = bus;
        _cpuMap = cpuMap;
        _gpuMap = gpuMap;
        _ramMap = ramMap;
    }

    public async Task Handle(CpuSampleArrivedEvent message)
    {
        var dto = _cpuMap.Map(message.CpuSample);

        await _hub.Clients.All.SendAsync(ReporterHub.CpuCollectionMethod, dto);
    }

    public async Task Handle(GpuSampleArrivedEvent message)
    {
        var dto = _gpuMap.Map(message.GpuSample);

        await _hub.Clients.All.SendAsync(ReporterHub.GpuCollectionMethod, dto);
    }

    public async Task Handle(RamSampleArrivedEvent message)
    {
        var dto = _ramMap.Map(message.RamSample);

        await _hub.Clients.All.SendAsync(ReporterHub.RamCollectionMethod, dto);
    }
}