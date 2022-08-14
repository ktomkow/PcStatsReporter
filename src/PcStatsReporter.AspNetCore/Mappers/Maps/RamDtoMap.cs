using System;
using PcStatsReporter.AspNetCore.SignalR.Contracts;
using PcStatsReporter.Core.Maps;
using PcStatsReporter.Core.Models;

namespace PcStatsReporter.AspNetCore.Mappers.Maps;

[Obsolete]
public class RamDtoMap : IMap<RamSample, RamSampleDto>
{
    public RamSampleDto Map(RamSample source)
    {
        RamSampleDto result = new();

        result.InUse = source.InUse;

        return result;
    }
}