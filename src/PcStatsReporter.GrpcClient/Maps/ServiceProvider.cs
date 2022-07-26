using Microsoft.Extensions.DependencyInjection;
using PcStatsReporter.Core.Maps;
using PcStatsReporter.Core.Models;
using PcStatsReporter.Grpc.Proto;

namespace PcStatsReporter.GrpcClient.Maps;

public static class ServiceProvider
{
    public static void AddGrpcClientMaps(this IServiceCollection services)
    {
        services.AddTransient<IMap<CpuSample, CollectedData>, CpuSampleMap>();
    }
}