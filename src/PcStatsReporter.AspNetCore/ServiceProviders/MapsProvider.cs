using Microsoft.Extensions.DependencyInjection;
using PcStatsReporter.AspNetCore.Mappers;
using PcStatsReporter.AspNetCore.Mappers.Maps;
using PcStatsReporter.Core.Models;
using PcStatsReporter.RestContracts;

namespace PcStatsReporter.AspNetCore.ServiceProviders;

public static class MapsProvider
{
    public static void AddMaps(this IServiceCollection services)
    {
        services.AddSingleton<IMap<CpuData, CpuResponse>, CpuMap>();
        services.AddSingleton<IMap<RamData, RamResponse>, RamMap>();
        services.AddSingleton<IMap<GpuData, GpuResponse>, GpuMap>();
    }
}