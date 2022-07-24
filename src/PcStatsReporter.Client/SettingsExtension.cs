using PcStatsReporter.Core.ReportingClientSettings;

namespace PcStatsReporter.Client;

public static class SettingsExtension
{
    public static CpuCollectSettings? GetCpuSettings(this ICollection<ReportingClientSettings> settings)
    {
        return settings.FirstOrDefault(x => x.GetType() == typeof(CpuCollectSettings)) as CpuCollectSettings;
    }
    
    public static RamCollectSettings? GetRamSettings(this ICollection<ReportingClientSettings> settings)
    {
        return settings.FirstOrDefault(x => x.GetType() == typeof(RamCollectSettings)) as RamCollectSettings;
    }
    
    public static GpuCollectSettings? GetGpuSettings(this ICollection<ReportingClientSettings> settings)
    {
        return settings.FirstOrDefault(x => x.GetType() == typeof(GpuCollectSettings)) as GpuCollectSettings;
    }
    
    public static SettingsRefreshSettings? GetServiceSettings(this ICollection<ReportingClientSettings> settings)
    {
        return settings.FirstOrDefault(x => x.GetType() == typeof(SettingsRefreshSettings)) as SettingsRefreshSettings;
    }
}