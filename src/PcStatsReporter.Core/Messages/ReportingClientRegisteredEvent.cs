using System;
using PcStatsReporter.Core.Models;

namespace PcStatsReporter.Core.Messages;

public class ReportingClientRegisteredEvent
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public PcInfo PcInfo { get; set; }
}