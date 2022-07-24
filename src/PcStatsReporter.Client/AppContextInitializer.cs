using Microsoft.Extensions.Logging;

namespace PcStatsReporter.Client;

public class AppContextInitializer : Initializer<AppContext>
{
    private readonly IInitializer<ClientChannel> _grpcInitializer;
    private readonly IInitializer<Settings> _settingsInitializer;
    private readonly IInitializer<SettingsCollector> _settingsCollectorInitializer;
    private readonly SettingsCollector _settingsCollector;

    public AppContextInitializer(
        ILogger<AppContextInitializer> logger,
        IInitializer<ClientChannel> grpcInitializer,
        IInitializer<Settings> settingsInitializer,
        IInitializer<SettingsCollector> settingsCollectorInitializer,
        SettingsCollector settingsCollector) : base(logger)
    {
        _grpcInitializer = grpcInitializer;
        _settingsInitializer = settingsInitializer;
        _settingsCollectorInitializer = settingsCollectorInitializer;
        _settingsCollector = settingsCollector;
    }

    protected override async Task InitializeResult(AppContext initializable)
    {
        await Task.Delay(TimeSpan.FromSeconds(5));
        await _grpcInitializer.Initialize(initializable.ClientChannel);
        await _settingsCollectorInitializer.Initialize(_settingsCollector);
        await _settingsInitializer.Initialize(initializable.Settings);
        
        await Task.Delay(1);
    }
}