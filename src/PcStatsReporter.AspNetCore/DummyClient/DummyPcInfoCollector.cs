using PcStatsReporter.Core.Models;

namespace PcStatsReporter.AspNetCore.DummyClient;

public class DummyPcInfoCollector : ICollector<PcInfo>
{
    private readonly DummyClientSettings _settings;

    public DummyPcInfoCollector(DummyClientSettings settings)
    {
        _settings = settings;
    }
    
    public PcInfo Collect()
    {
        return new PcInfo()
        {
            CpuName = _settings.CpuName,
            GpuName = _settings.GpuName,
            TotalRam = _settings.TotalRam
        };
    }
}