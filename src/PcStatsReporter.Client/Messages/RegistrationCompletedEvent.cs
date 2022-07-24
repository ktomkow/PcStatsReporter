using PcStatsReporter.Core.ReportingClientSettings;

namespace PcStatsReporter.Client.Messages;

public class RegistrationCompletedEvent
{
    public CpuCollectSettings CpuCollectSettings { get; set; }
    public GpuCollectSettings GpuCollectSettings { get; set; }
    public RamCollectSettings RamCollectSettings { get; set; }
    public SettingsRefreshSettings SettingsRefreshSettings { get; set; }
}