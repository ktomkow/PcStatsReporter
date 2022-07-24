using Microsoft.Extensions.Logging;

namespace PcStatsReporter.Client;

public class SettingCollectorInitializer : Initializer<SettingsCollector>
{
    public SettingCollectorInitializer(ILogger logger) : base(logger)
    {
    }

    protected override async Task InitializeResult(SettingsCollector initializable)
    {
        await initializable.SelfInit();
    }
}