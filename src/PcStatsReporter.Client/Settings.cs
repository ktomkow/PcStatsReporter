using PcStatsReporter.Core.ReportingClientSettings;

namespace PcStatsReporter.Client;

public class Settings : Initializable
{
    private bool _isInitialized;

    public void MarkAsInitialized()
    {
        _isInitialized = true;
    }

    public CpuCollectSettings CpuCollectSettings { get; set; }
    public GpuCollectSettings GpuCollectSettings { get; set;}
    public RamCollectSettings RamCollectSettings { get; set;}
    public ReportingClientSettings ReportingClientSettings { get; set;}

    public override async Task<bool> IsInitialized()
    {
        return await Task.FromResult(_isInitialized);
    }
}