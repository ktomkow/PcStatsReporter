using Microsoft.Extensions.Hosting;

namespace PcStatsReporter.Client;

public class InitializeService : BackgroundService
{
    private readonly AppContext _appContext;
    private readonly IInitializer<AppContext> _appContextInitializer;

    public InitializeService(AppContext appContext, IInitializer<AppContext> appContextInitializer)
    {
        _appContext = appContext;
        _appContextInitializer = appContextInitializer;
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await _appContextInitializer.Initialize(_appContext);
    }
}