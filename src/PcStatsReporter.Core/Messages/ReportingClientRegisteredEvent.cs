using System;

namespace PcStatsReporter.Core.Messages;

public class ReportingClientRegisteredEvent
{
    public Guid Id { get; set; } = Guid.NewGuid();
}