using PcStatsReporter.Core.ReportingClientSettings;

namespace PcStatsReporter.Client;

public class Settings
{
    public CpuCollectSettings CpuCollectSettings { get; }
    public GpuCollectSettings GpuCollectSettings { get; }
    public RamCollectSettings RamCollectSettings { get; }
    public SettingsRefreshSettings SettingsRefreshSettings { get; }

    public Settings(CpuCollectSettings cpuCollectSettings, GpuCollectSettings gpuCollectSettings, RamCollectSettings ramCollectSettings, SettingsRefreshSettings settingsRefreshSettings)
    {
        CpuCollectSettings = cpuCollectSettings;
        GpuCollectSettings = gpuCollectSettings;
        RamCollectSettings = ramCollectSettings;
        SettingsRefreshSettings = settingsRefreshSettings;
    }
}