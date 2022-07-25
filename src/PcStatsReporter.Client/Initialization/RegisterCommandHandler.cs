using Google.Protobuf.Collections;
using PcStatsReporter.Client.Messages;
using PcStatsReporter.Core.ReportingClientSettings;
using PcStatsReporter.Grpc.Proto;
using Rebus.Bus;
using Rebus.Handlers;

namespace PcStatsReporter.Client.Initialization;

public class RegisterCommandHandler : IHandleMessages<RegisterCommand>
{
    private readonly AppContext _appContext;
    private readonly IBus _bus;

    public RegisterCommandHandler(AppContext appContext, IBus bus)
    {
        _appContext = appContext;
        _bus = bus;
    }
    
    public async Task Handle(RegisterCommand message)
    { 
        var settings = await Register(message);

        var @event = new RegistrationCompletedEvent()
        {
            CpuCollectSettings = settings.CpuCollectSettings,
            GpuCollectSettings = settings.GpuCollectSettings,
            RamCollectSettings = settings.RamCollectSettings,
            SettingsRefreshSettings = settings.SettingsRefreshSettings
        };

        await _bus.Publish(@event);
    }
    
    private async Task<Settings> Register(RegisterCommand message)
    {
        var grpcChannel = _appContext.ClientChannel;
        var registerClient = new Registerer.RegistererClient(grpcChannel);
    
        var request = new RegistrationRequest()
        {
            CpuName = message.CpuName,
            GpuName = message.GpuName,
            RamCapacity = (float)message.TotalRam
        };
        
        RegistrationResponse response = await registerClient.RegisterAsync(request);
    
        return MapSettings(response.Settings.Settings);
    }
    
    private Settings MapSettings(RepeatedField<Setting> settingsSettings)
    {
        var cpuSettings = new CpuCollectSettings();
        var serviceSettings = new SettingsRefreshSettings();
        var gpuSettings = new GpuCollectSettings();
        var ramSettings = new RamCollectSettings();
        
        foreach (Setting setting in settingsSettings)
        {
            switch (setting.Sensor)
            {
                case SettingType.Service:
                    serviceSettings.Period = TimeSpan.FromSeconds(setting.Period);
                    break;
    
                case SettingType.Cpu:
                    cpuSettings.Period = TimeSpan.FromSeconds(setting.Period);
                    break;
    
                case SettingType.Gpu:
                    gpuSettings.Period = TimeSpan.FromSeconds(setting.Period);
                    break;
    
                case SettingType.Ram:
                    ramSettings.Period = TimeSpan.FromSeconds(setting.Period);
                    break;
    
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    
        return new Settings(cpuSettings, gpuSettings, ramSettings, serviceSettings);
    }
}