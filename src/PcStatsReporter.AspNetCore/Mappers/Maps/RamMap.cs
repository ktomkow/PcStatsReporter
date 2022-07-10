using PcStatsReporter.Core.Models;
using PcStatsReporter.RestContracts;

namespace PcStatsReporter.AspNetCore.Mappers.Maps;

public class RamMap : IMap<RamData, RamResponse>
{
    public RamResponse Map(RamData source)
    {
        RamResponse result = new RamResponse()
        {
            Total = source.Total,
            Used = source.Used
        };

        return result;
    }
}