using PcStatsReporter.Core.Models;

namespace PcStatsReporter.LibreHardware;

public interface ICollector<T> where T : Sample
{
    T Collect();
}