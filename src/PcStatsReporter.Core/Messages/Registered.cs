using System;

namespace PcStatsReporter.Core.Messages;

public class Registered
{
    public Guid Id { get; set; } = Guid.NewGuid();
}