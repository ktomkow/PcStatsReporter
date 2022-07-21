using System;

namespace PcStatsReporter.Core.ReportingClientSettings
{
    public abstract class ReportingClientSettings
    {
        public bool Enabled { get; set; }
        public TimeSpan Period { get; set; }
    }
}