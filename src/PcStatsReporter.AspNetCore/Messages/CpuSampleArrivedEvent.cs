using PcStatsReporter.Core.Messages;
using PcStatsReporter.Core.Models;

namespace PcStatsReporter.AspNetCore.Messages;

public class CpuSampleArrivedEvent : IEvent
{
    public CpuSample CpuSample { get; set; }
}