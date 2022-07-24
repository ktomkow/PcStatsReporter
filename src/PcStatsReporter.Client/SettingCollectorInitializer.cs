using Microsoft.Extensions.Logging;

namespace PcStatsReporter.Client;

public class SettingCollectorInitializer : Initializer<SettingsCollector>
{
    private readonly AppContext _appContext;

    public SettingCollectorInitializer(
        ILogger<SettingCollectorInitializer> logger, 
        AppContext appContext) : base(logger)
    {
        _appContext = appContext;
    }

    protected override async Task InitializeResult(SettingsCollector initializable)
    {
        await initializable.Init(_appContext.ClientChannel);
    }
}