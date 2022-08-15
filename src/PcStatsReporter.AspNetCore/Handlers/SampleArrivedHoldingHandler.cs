using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using PcStatsReporter.Core.Messages;
using PcStatsReporter.Core.Persistence;
using Rebus.Handlers;

namespace PcStatsReporter.AspNetCore.Handlers;

public class SampleArrivedHoldingHandler : IHandleMessages<CpuSampleArrivedEvent>, IHandleMessages<GpuSampleArrivedEvent>, IHandleMessages<RamSampleArrivedEvent>
{
    private readonly ILogger<SampleArrivedHoldingHandler> _logger;
    private readonly IHold _holder;

    public SampleArrivedHoldingHandler(ILogger<SampleArrivedHoldingHandler> logger, IHold holder)
    {
        _logger = logger;
        _holder = holder;
    }
    
    public async Task Handle(CpuSampleArrivedEvent message)
    {
        _logger.LogDebug("Setting latest {Type}", message.CpuSample.GetType().Name);
        await _holder.Set(message.CpuSample);
    }

    public async Task Handle(GpuSampleArrivedEvent message)
    {
        _logger.LogDebug("Setting latest {Type}", message.GpuSample.GetType().Name);
        await _holder.Set(message.GpuSample);
    }

    public async Task Handle(RamSampleArrivedEvent message)
    {
        _logger.LogDebug("Setting latest {Type}", message.RamSample.GetType().Name);
        await _holder.Set(message.RamSample);
    }
}