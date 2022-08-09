using Microsoft.Extensions.DependencyInjection;
using PcStatsReporter.AspNetCore.Mappers.Maps;
using PcStatsReporter.AspNetCore.Mappers.ProtoRelatedMaps;
using PcStatsReporter.AspNetCore.SignalR.Contracts;
using PcStatsReporter.Core.Maps;
using PcStatsReporter.Core.Models;
using PcStatsReporter.Grpc.Proto;

namespace PcStatsReporter.AspNetCore.Mappers;

public static class MapsProvider
{
    public static void AddServerMaps(this IServiceCollection services)
    {
        services.AddTransient<IMap<CollectedData, CpuSample>, CollectedCpuSampleMap>();
        services.AddTransient<IMap<CollectedData, GpuSample>, CollectedGpuSampleMap>();
        services.AddTransient<IMap<CollectedData, RamSample>, CollectedRamSampleMap>();
        
        services.AddTransient<IMap<PcInfo, PcInfoDto>, PcInfoMap>();
        services.AddTransient<IMap<CpuSample, CpuSampleDto>, CpuDtoMap>();
        services.AddTransient<IMap<GpuSample, GpuSampleDto>, GpuDtoMap>();
        services.AddTransient<IMap<RamSample, RamSampleDto>, RamDtoMap>();
    }
}