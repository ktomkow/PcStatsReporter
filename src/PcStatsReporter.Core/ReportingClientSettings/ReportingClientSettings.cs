using System;

namespace PcStatsReporter.Core.ReportingClientSettings
{
    public abstract class ReportingClientSettings
    {
        public TimeSpan Period { get; set; }
    }
}