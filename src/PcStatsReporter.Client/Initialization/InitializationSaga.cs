using Microsoft.Extensions.Logging;
using PcStatsReporter.Client.Messages;
using Rebus.Bus;
using Rebus.Handlers;
using Rebus.Sagas;

namespace PcStatsReporter.Client.Initialization;

public class InitializationSaga : 
    IAmInitiatedBy<InitializeCommand>, 
    IHandleMessages<GotRegistrationDataEvent>,
    IHandleMessages<RegistrationCompletedEvent>, 
    IHandleMessages<GrpcInitializedEvent>
{
    private readonly AppContext _appContext;
    private readonly ILogger<InitializationSaga> _logger;
    private readonly IBus _bus;

    public InitializationSaga(IBus bus, AppContext appContext, ILogger<InitializationSaga> logger)
    {
        _bus = bus;
        _appContext = appContext;
        _logger = logger;
    }

    public async Task Handle(InitializeCommand message)
    {
        await _bus.Publish(new GrpcInitializeCommand());
    }

    public async Task Handle(GrpcInitializedEvent message)
    {
        await _bus.Publish(new GetRegistrationDataCommand());
    }

    public async Task Handle(GotRegistrationDataEvent message)
    {
        var command = new RegisterCommand()
        {
            CpuName = message.CpuName,
            GpuName = message.GpuName,
            TotalRam = message.TotalRam
        };

        await _bus.Publish(command);
    }

    public async Task Handle(RegistrationCompletedEvent message)
    {
        Settings settings = new Settings(
            message.CpuCollectSettings, 
            message.GpuCollectSettings,
            message.RamCollectSettings, 
            message.SettingsRefreshSettings);
        
        _appContext.SetSettings(settings);
        
        await _appContext.Initialize();
        
        _logger.LogInformation("Register Saga Completed");
        await Task.CompletedTask;
    }
}