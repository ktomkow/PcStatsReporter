using PcStatsReporter.Core.Models;

namespace PcStatsReporter.Core.Messages;

public class GpuSampleArrivedEvent : IEvent
{
    public GpuSample GpuSample { get; set; }
}