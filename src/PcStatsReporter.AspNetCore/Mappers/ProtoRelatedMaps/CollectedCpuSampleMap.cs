using System;
using PcStatsReporter.Core.Maps;
using PcStatsReporter.Core.Models;
using PcStatsReporter.Grpc.Proto;

namespace PcStatsReporter.AspNetCore.Mappers.ProtoRelatedMaps;

public class CollectedCpuSampleMap : IMap<CollectedData, CpuSample>
{
    public CpuSample Map(CollectedData source)
    {
        
        var result = new CpuSample()
        {
            Id = System.Guid.Parse(source.Uuid.Value),
            RegisteredAt = DateTimeOffset.FromUnixTimeSeconds(source.Timestamp).UtcDateTime, 
            Temperature = source.Cpu.Temperature,
            AverageLoad = source.Cpu.AverageLoad,
        };

        foreach (var sourceCore in source.Cpu.Cores)
        {
            var core = new CoreSample()
            {
                Speed = sourceCore.Speed,
                Temperature = sourceCore.Temperature,
                CoreNumber = sourceCore.Id
            };

            foreach (var sourceThread in sourceCore.Threads)
            {
                core.ThreadsLoad.Add((threadNumber: sourceThread.Id, threadLoad: sourceThread.Load));
            }
            
            result.Cores.Add(core);
        }
        
        return result;
    }
}