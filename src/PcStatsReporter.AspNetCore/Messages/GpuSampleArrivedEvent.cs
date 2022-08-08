using PcStatsReporter.Core.Messages;
using PcStatsReporter.Core.Models;

namespace PcStatsReporter.AspNetCore.Messages;

public class GpuSampleArrivedEvent : IEvent
{
    public GpuSample GpuSample { get; set; }
}