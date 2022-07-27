using PcStatsReporter.AspNetCore.SignalR.Contracts;
using PcStatsReporter.Core.Maps;
using PcStatsReporter.Core.Models;

namespace PcStatsReporter.AspNetCore.Mappers.Maps;

public class FooMap : IMap<CpuSample, Foo>
{
    public Foo Map(CpuSample source)
    {
        throw new System.NotImplementedException();
    }
}