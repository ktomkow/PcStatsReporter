using PcStatsReporter.AspNetCore.Configuration;

namespace PcStatsReporter.AspNetCore;

public class ServiceSettings : IWebSettings
{
    public bool UseDummyClient { get; set; }
}