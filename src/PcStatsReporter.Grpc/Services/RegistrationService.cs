using System.Threading.Tasks;
using Grpc.Core;
using PcStatsReporter.Core.Models;
using PcStatsReporter.Core.Persistence;
using PcStatsReporter.Core.ReportingClientSettings;
using PcStatsReporter.Grpc.Proto;

namespace PcStatsReporter.Grpc.Services;

public class RegistrationService : Registerer.RegistererBase
{
    private readonly IHold _hold;

    public RegistrationService(IHold hold)
    {
        _hold = hold;
    }

    public override async Task<RegistrationResponse> Register(Registration request, ServerCallContext context)
    {
        PcInfo pcInfo = new PcInfo()
        {
            CpuName = request.CpuName,
            GpuName = request.GpuName,
            TotalRam = request.RamCapacity
        };

        RegistrationResponse response = new RegistrationResponse();
        response.Settings = new SettingsResponse();

        var cpuSettings = await _hold.Get<CpuCollectSettings>();
        var gpuSettings = await _hold.Get<GpuCollectSettings>();
        var ramSettings = await _hold.Get<RamCollectSettings>();
        var serviceSettings = await _hold.Get<SettingsRefreshSettings>();
        
        await _hold.Set(pcInfo);

        response.Settings.Settings.Add(new Setting()
        {
            Sensor = SettingType.Cpu,
            Period = (uint) cpuSettings.Period.TotalSeconds
        });

        response.Settings.Settings.Add(new Setting()
        {
            Sensor = SettingType.Gpu,
            Period = (uint) gpuSettings.Period.TotalSeconds
        });

        response.Settings.Settings.Add(new Setting()
        {
            Sensor = SettingType.Ram,
            Period = (uint) ramSettings.Period.TotalSeconds
        });

        response.Settings.Settings.Add(new Setting()
        {
            Sensor = SettingType.Service,
            Period = (uint) serviceSettings.Period.TotalSeconds
        });
        
        return response;
    }
}