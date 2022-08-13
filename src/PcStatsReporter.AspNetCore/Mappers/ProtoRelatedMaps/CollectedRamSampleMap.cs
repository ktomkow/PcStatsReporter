using PcStatsReporter.Core.Maps;
using PcStatsReporter.Core.Models;
using PcStatsReporter.Grpc.Proto;

namespace PcStatsReporter.AspNetCore.Mappers.ProtoRelatedMaps;

public class CollectedRamSampleMap : IMap<CollectedData, RamSample>
{
    public RamSample Map(CollectedData source)
    {
        var result = new RamSample()
        {
            InUse = source.Ram.InUse
        };

        return result;
    }
}