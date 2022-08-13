using PcStatsReporter.Core.Messages;
using PcStatsReporter.Core.Models;

namespace PcStatsReporter.AspNetCore.Messages;

public class RamSampleArrivedEvent : IEvent
{
    public RamSample RamSample { get; set; }
}