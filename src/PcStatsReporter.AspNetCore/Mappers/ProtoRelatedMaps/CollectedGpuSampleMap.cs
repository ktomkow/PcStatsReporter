using System;
using PcStatsReporter.Core.Maps;
using PcStatsReporter.Core.Models;
using PcStatsReporter.Grpc.Proto;

namespace PcStatsReporter.AspNetCore.Mappers.ProtoRelatedMaps;

public class CollectedGpuSampleMap : IMap<CollectedData, GpuSample>
{
    public GpuSample Map(CollectedData source)
    {
        var result = new GpuSample()
        {
            Id = System.Guid.Parse(source.Uuid.Value),
            RegisteredAt = DateTimeOffset.FromUnixTimeSeconds(source.Timestamp).UtcDateTime,
            CoreTemperature = source.Gpu.CoreTemperature,
            GpuCoreLoad = source.Gpu.CoreLoad
            // todo: missing properties
        };

        return result;
    }
}