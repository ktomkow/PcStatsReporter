using Microsoft.Extensions.Hosting;
using PcStatsReporter.Client.Messages;
using Rebus.Handlers;

namespace PcStatsReporter.Client;

public class SettingsService : BackgroundService, IHandleMessages<SettingsChanged>
{
    private readonly AppContext _appContext;

    public SettingsService(AppContext appContext)
    {
        _appContext = appContext;
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await _appContext.WaitForInitialization();
        
        // when initialization is done - run getting settings
        await Task.CompletedTask;
    }

    public Task Handle(SettingsChanged message)
    {
        throw new NotImplementedException();
    }
}