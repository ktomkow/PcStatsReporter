using PcStatsReporter.AspNetCore.SignalR.Contracts;
using PcStatsReporter.Core.Maps;
using PcStatsReporter.Core.Models;

namespace PcStatsReporter.AspNetCore.Mappers.Maps;

public class PcInfoMap : IMap<PcInfo, PcInfoDto>
{
    public PcInfoDto Map(PcInfo source)
    {
        return new PcInfoDto()
        {
            CpuName = source.CpuName,
            GpuName = source.GpuName,
            TotalRam = source.TotalRam
        };
    }
}