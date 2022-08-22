using System;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using PcStatsReporter.Core.Messages;
using PcStatsReporter.Core.Models;
using PcStatsReporter.Core.Persistence;
using PcStatsReporter.Core.ReportingClientSettings;
using PcStatsReporter.Grpc.Proto;
using Rebus.Bus;

namespace PcStatsReporter.Grpc.Services;

public class RegistrationService : Registerer.RegistererBase
{
    private readonly ILogger<RegistrationService> _logger;
    private readonly IHold _hold;
    private readonly ReportingClientSettings _defaultSetting;
    private readonly IBus _bus;

    public RegistrationService(ILogger<RegistrationService> logger, IHold hold, DefaultSetting defaultSetting, IBus bus)
    {
        _logger = logger;
        _hold = hold;
        _defaultSetting = defaultSetting;
        _bus = bus;
    }

    public override async Task<RegistrationResponse> Register(RegistrationRequest request, ServerCallContext context)
    {
        PcInfo pcInfo = new PcInfo()
        {
            CpuName = request.CpuName,
            GpuName = request.GpuName,
            TotalRam = request.RamCapacity
        };

        _logger.LogInformation("Registering PcInfo {@PcInfo}", pcInfo);

        RegistrationResponse response = new RegistrationResponse();
        response.Settings = new SettingsResponse();

        // todo: create service/etc with default settings
        ReportingClientSettings cpuSettings = await _hold.Get<CpuCollectSettings>() ?? _defaultSetting;
        ReportingClientSettings gpuSettings = await _hold.Get<GpuCollectSettings>() ?? _defaultSetting;
        ReportingClientSettings ramSettings = await _hold.Get<RamCollectSettings>() ?? _defaultSetting;
        ReportingClientSettings serviceSettings = await _hold.Get<SettingsRefreshSettings>() ?? _defaultSetting;

        await _hold.Set(pcInfo); // todo: to remove

        response.Settings.Settings.Add(new Setting()
        {
            Sensor = SettingType.Cpu,
            Period = (uint) cpuSettings.Period.TotalMilliseconds
        });

        response.Settings.Settings.Add(new Setting()
        {
            Sensor = SettingType.Gpu,
            Period = (uint) gpuSettings.Period.TotalMilliseconds
        });

        response.Settings.Settings.Add(new Setting()
        {
            Sensor = SettingType.Ram,
            Period = (uint) ramSettings.Period.TotalMilliseconds
        });

        response.Settings.Settings.Add(new Setting()
        {
            Sensor = SettingType.Service,
            Period = (uint) serviceSettings.Period.TotalMilliseconds
        });
        
        await _bus.Publish(new ReportingClientRegisteredEvent()
        {
            PcInfo = pcInfo
        });

        return response;
    }
}