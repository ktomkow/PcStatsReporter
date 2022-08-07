using PcStatsReporter.Core.Models;

namespace PcStatsReporter.AspNetCore.Messages;

public class CpuSampleArrivedEvent
{
    public CpuSample CpuSample { get; set; }
}