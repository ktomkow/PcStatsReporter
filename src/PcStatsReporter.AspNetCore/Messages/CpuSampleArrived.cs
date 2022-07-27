using PcStatsReporter.Core.Models;

namespace PcStatsReporter.AspNetCore.Messages;

public class CpuSampleArrived
{
    public CpuSample CpuSample { get; set; }
}