using PcStatsReporter.Core.Models;

namespace PcStatsReporter.Core.Messages;

public class CpuSampleArrivedEvent : IEvent
{
    public CpuSample CpuSample { get; set; }
}