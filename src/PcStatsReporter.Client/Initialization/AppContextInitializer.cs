using Microsoft.Extensions.Logging;
using PcStatsReporter.Grpc.Proto;

namespace PcStatsReporter.Client.Initialization;

public class AppContextInitializer : Initializer<AppContext>
{
    private readonly IInitializer<ClientChannel> _grpcInitializer;
    private readonly IInitializer<SettingsCollector> _settingsCollectorInitializer;
    private readonly SettingsCollector _settingsCollector;

    public AppContextInitializer(
        ILogger<AppContextInitializer> logger,
        IInitializer<ClientChannel> grpcInitializer,
        IInitializer<SettingsCollector> settingsCollectorInitializer,
        SettingsCollector settingsCollector) : base(logger)
    {
        _grpcInitializer = grpcInitializer;
        _settingsCollectorInitializer = settingsCollectorInitializer;
        _settingsCollector = settingsCollector;
    }

    protected override async Task InitializeResult(AppContext initializable)
    {
        await Task.Delay(TimeSpan.FromSeconds(5));
        await _grpcInitializer.Initialize(initializable.ClientChannel);
        await Register(initializable);
        await _settingsCollectorInitializer.Initialize(_settingsCollector);
        
        await Task.Delay(1);
    }

    private async Task Register(AppContext appContext)
    {
        var client = new Registerer.RegistererClient(appContext.ClientChannel.GrpcChannel);
        var request = new RegistrationRequest();
        request.CpuName = "Dupny";
        request.GpuName = "Nijaka";
        request.RamCapacity = 15.9f;

        var response = await client.RegisterAsync(request);

        var mappedSettings = SettingsCollector.Map(response.Settings);

        appContext.Settings.CpuCollectSettings.Period = mappedSettings.GetCpuSettings().Period;
        appContext.Settings.GpuCollectSettings.Period = mappedSettings.GetGpuSettings().Period;
        appContext.Settings.RamCollectSettings.Period = mappedSettings.GetRamSettings().Period;
        appContext.Settings.SettingsRefreshSettings.Period = mappedSettings.GetServiceSettings().Period;
    }
}