using Microsoft.Extensions.Hosting;

namespace PcStatsReporter.Client;

public class SettingsService : BackgroundService
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
}