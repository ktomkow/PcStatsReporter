using System.Collections.Generic;
using System.Linq;
using PcStatsReporter.Core.Models;
using PcStatsReporter.RestContracts;

namespace PcStatsReporter.AspNetCore.Mappers.Maps;

public class CpuMap : IMap<CpuData, CpuResponse>
{
    public CpuResponse Map(CpuData source)
    {
        CpuResponse result = new()
        {
            Name = source.Name,
            Cores = MapCores(source.Cores)
        };
        
        return result;
    }

    private static List<CpuCoreResponse> MapCores(IEnumerable<CpuCore> cores)
    {
        return cores.Select(x => new CpuCoreResponse()
            {
                Id = x.Id,
                Load = x.Load,
                Speed = x.Speed,
                Temperature = x.Temperature
            })
            .ToList();
    }
}