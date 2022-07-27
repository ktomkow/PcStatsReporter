using Microsoft.Extensions.DependencyInjection;
using PcStatsReporter.AspNetCore.Mappers.ProtoRelatedMaps;
using PcStatsReporter.Core.Maps;
using PcStatsReporter.Core.Models;
using PcStatsReporter.Grpc.Proto;

namespace PcStatsReporter.AspNetCore.Mappers;

public static class MapsProvider
{
    public static void AddServerMaps(this IServiceCollection services)
    {
        services.AddTransient<IMap<CollectedData, CpuSample>, CollectedCpuSampleMap>();
    }
}