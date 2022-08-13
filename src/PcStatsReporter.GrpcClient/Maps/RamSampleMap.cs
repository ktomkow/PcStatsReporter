using System;
using PcStatsReporter.Core.Maps;
using PcStatsReporter.Core.Models;
using PcStatsReporter.Grpc.Proto;
using Guid = PcStatsReporter.Grpc.Proto.Guid;

namespace PcStatsReporter.GrpcClient.Maps;

public class RamSampleMap : IMap<RamSample, CollectedData>
{
    public CollectedData Map(RamSample source)
    {
        CollectedData result = new();
        result.Uuid = new Guid();
        result.Uuid.Value = source.Id.ToString();
        result.Timestamp = (uint) ((DateTimeOffset) source.RegisteredAt.ToUniversalTime()).ToUnixTimeSeconds();

        CollectedRamData ramData = new CollectedRamData()
        {
            InUse = (float)source.InUse
        };

        result.Ram = ramData;
        
        return result;
    }
}