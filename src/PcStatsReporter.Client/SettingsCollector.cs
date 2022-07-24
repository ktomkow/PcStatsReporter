using PcStatsReporter.Core.ReportingClientSettings;
using PcStatsReporter.Grpc.Proto;

namespace PcStatsReporter.Client;

public class SettingsCollector
{
    private readonly AppContext _appContext;
    private SettingsManager.SettingsManagerClient _client;

    public SettingsCollector(AppContext appContext)
    {
        _appContext = appContext;
    }
    
    public async Task<List<ReportingClientSettings>> Get()
    {
        await _appContext.WaitForInitialization();
        if (_client is null)
        {
            await SelfInit();
        }

        SettingsRequest request = new SettingsRequest();
        SettingsResponse response = await _client.GetAsync(request);

        var list = new List<ReportingClientSettings>();

        foreach (var setting in response.Settings)
        {
            switch (setting.Sensor)
            {
                case SettingType.Service:
                    var serviceSettings = new SettingsRefreshSettings()
                    {
                        Period = TimeSpan.FromSeconds(setting.Period)
                    };
                    list.Add(serviceSettings);
                    break;
                
                case SettingType.Cpu:
                    var cpuSettings = new CpuCollectSettings()
                    {
                        Period = TimeSpan.FromSeconds(setting.Period)
                    };
                    list.Add(cpuSettings);
                    break;
                
                case SettingType.Gpu:
                    var gpuSettings = new GpuCollectSettings()
                    {
                        Period = TimeSpan.FromSeconds(setting.Period)
                    };
                    list.Add(gpuSettings);
                    break;
                
                case SettingType.Ram:
                    var ramSettings = new RamCollectSettings()
                    {
                        Period = TimeSpan.FromSeconds(setting.Period)
                    };
                    list.Add(ramSettings);
                    break;
                
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        return list;
    }

    private async Task SelfInit()
    {
        await _appContext.ClientChannel.WaitForInitialization();
        _client = new SettingsManager.SettingsManagerClient(_appContext.ClientChannel.GrpcChannel);
    }
}