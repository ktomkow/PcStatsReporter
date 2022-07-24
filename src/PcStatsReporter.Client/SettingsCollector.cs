using PcStatsReporter.Core.ReportingClientSettings;

namespace PcStatsReporter.Client;

public class SettingsCollector
{
    private readonly AppContext _appContext;

    public SettingsCollector(AppContext appContext)
    {
        _appContext = appContext;
    }
    
    public async Task<List<ReportingClientSettings>> Get()
    {
        throw new Exception();
    }
}