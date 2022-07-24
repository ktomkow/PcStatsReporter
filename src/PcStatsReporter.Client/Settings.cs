using PcStatsReporter.Core.ReportingClientSettings;

namespace PcStatsReporter.Client;

public class Settings : Initializable
{
    public CpuCollectSettings CpuCollectSettings { get; }
    public GpuCollectSettings GpuCollectSettings { get; }
    public RamCollectSettings RamCollectSettings { get; }
    public SettingsRefreshSettings SettingsRefreshSettings { get; }

    public Settings()
    {
        CpuCollectSettings = new CpuCollectSettings();
        GpuCollectSettings = new GpuCollectSettings();
        RamCollectSettings = new RamCollectSettings();
        SettingsRefreshSettings = new SettingsRefreshSettings();
    }

    public override async Task<bool> IsInitialized()
    {
        return await Task.FromResult(CpuCollectSettings.Period != TimeSpan.Zero &&
                                     GpuCollectSettings.Period != TimeSpan.Zero &&
                                     RamCollectSettings.Period != TimeSpan.Zero &&
                                     SettingsRefreshSettings.Period != TimeSpan.Zero);
    }
}