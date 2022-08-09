using PcStatsReporter.AspNetCore.SignalR.Contracts;
using PcStatsReporter.Core.Maps;
using PcStatsReporter.Core.Models;

namespace PcStatsReporter.AspNetCore.Mappers.Maps;

public class GpuDtoMap : IMap<GpuSample, GpuSampleDto>
{
    public GpuSampleDto Map(GpuSample source)
    {
        GpuSampleDto result = new();

        // todo: add missing
        result.CoreTemperature = source.CoreTemperature;
        
        return result;
    }
}