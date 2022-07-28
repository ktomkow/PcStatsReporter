using System.Collections.Generic;

namespace PcStatsReporter.AspNetCore.SignalR.Contracts;

public class CpuCoreSampleDto
{
    public uint CoreNumber { get; set; }
    public uint Temperature { get; set; }
    public uint Speed { get; set; }
    public IList<ThreadDto> ThreadsLoad { get; set; } = new List<ThreadDto>();
}