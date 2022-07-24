using Microsoft.Extensions.Logging;

namespace PcStatsReporter.Client;

public class SettingsInitializer : Initializer<Settings>
{
    private readonly AppContext _appContext;
    private readonly SettingsCollector _settingsCollector;

    public SettingsInitializer(ILogger<SettingsInitializer> logger, AppContext appContext, SettingsCollector settingsCollector) : base(logger)
    {
        _appContext = appContext;
        _settingsCollector = settingsCollector;
    }

    protected override async Task InitializeResult(Settings initializable)
    {
        await _settingsCollector.WaitForInitialization();
        var settings = await _settingsCollector.Get();

        _appContext.Settings.SettingsRefreshSettings.Period = settings.GetServiceSettings().Period;
        _appContext.Settings.CpuCollectSettings.Period = settings.GetCpuSettings().Period;
        _appContext.Settings.GpuCollectSettings.Period = settings.GetGpuSettings().Period;
        _appContext.Settings.RamCollectSettings.Period = settings.GetRamSettings().Period;
    }
}