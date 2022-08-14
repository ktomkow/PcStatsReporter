using System;
using System.Linq;
using PcStatsReporter.AspNetCore.SignalR.Contracts;
using PcStatsReporter.Core.Maps;
using PcStatsReporter.Core.Models;

namespace PcStatsReporter.AspNetCore.Mappers.Maps;

[Obsolete]
public class CpuDtoMap : IMap<CpuSample, CpuSampleDto>
{
    public CpuSampleDto Map(CpuSample source)
    {
        CpuSampleDto result = new();

        result.RegisteredAt = source.RegisteredAt;
        result.Temperature = source.Temperature;
        result.AverageLoad = source.AverageLoad;

        foreach (var sourceCore in source.Cores)
        {
            var core = new CpuCoreSampleDto()
            {
                CoreNumber = sourceCore.CoreNumber,
                Speed = sourceCore.Speed,
                Temperature = sourceCore.Temperature,
                ThreadsLoad = sourceCore.ThreadsLoad.Select(x => new ThreadDto()
                {
                    Number = x.threadNumber,
                    Load = x.threadLoad
                }).ToList()
            };
            
            result.Cores.Add(core);
        }
        
        
        return result;
    }
}