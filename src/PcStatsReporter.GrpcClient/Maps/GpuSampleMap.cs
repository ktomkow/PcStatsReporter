using System;
using PcStatsReporter.Core.Maps;
using PcStatsReporter.Core.Models;
using PcStatsReporter.Grpc.Proto;
using Guid = PcStatsReporter.Grpc.Proto.Guid;

namespace PcStatsReporter.GrpcClient.Maps;

public class GpuSampleMap : IMap<GpuSample, CollectedData>
{
    public CollectedData Map(GpuSample source)
    {
        CollectedData result = new();
        result.Uuid = new Guid();
        result.Uuid.Value = source.Id.ToString();
        result.Timestamp = (uint) ((DateTimeOffset) source.RegisteredAt.ToUniversalTime()).ToUnixTimeSeconds();

        CollectedGpuData gpuData = new CollectedGpuData()
        {
            CoreTemperature = source.CoreTemperature,
            CoreLoad = source.GpuCoreLoad
        };

        result.Gpu = gpuData;
        
        return result;
    }
}