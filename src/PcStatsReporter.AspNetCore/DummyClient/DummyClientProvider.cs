using Microsoft.Extensions.DependencyInjection;
using PcStatsReporter.Core.Models;

namespace PcStatsReporter.AspNetCore.DummyClient;

public static class DummyClientProvider
{
    public static void AddDummyClient(this IServiceCollection services)
    {
        services.AddSingleton<DummyClientSettings>();

        services.AddSingleton<ICollector<CpuSample>, DummyCpuClientCollector>();
        services.AddSingleton<ICollector<GpuSample>, DummyGpuClientCollector>();
        services.AddSingleton<ICollector<RamSample>, DummyRamClientCollector>();
        services.AddSingleton<ICollector<PcInfo>, DummyPcInfoCollector>();

        services.AddHostedService<DummyClientService>();
    }
}