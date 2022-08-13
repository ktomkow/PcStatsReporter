using System;
using System.Collections.Generic;

namespace PcStatsReporter.AspNetCore.SignalR.Contracts;

public class CpuSampleDto
{
    public DateTime RegisteredAt { get; set; }
    public uint Temperature { get; set; }
    public uint AverageLoad { get; set; }
    public IList<CpuCoreSampleDto> Cores { get; set; } = new List<CpuCoreSampleDto>();
}