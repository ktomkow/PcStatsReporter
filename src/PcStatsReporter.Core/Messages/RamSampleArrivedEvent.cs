using PcStatsReporter.Core.Models;

namespace PcStatsReporter.Core.Messages;

public class RamSampleArrivedEvent : IEvent
{
    public RamSample RamSample { get; set; }
}