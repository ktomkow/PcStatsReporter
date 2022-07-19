using PcStatsReporter.Core.Models;
using PcStatsReporter.RestContracts;

namespace PcStatsReporter.AspNetCore.Mappers;

public class GpuMap : IMap<GpuData, GpuResponse>
{
    public GpuResponse Map(GpuData source)
    {
        return new GpuResponse()
        {
            Name = source.Name,
            Temperature = source.Temperature,
            LoadCore = source.LoadCore
        };
    }
}