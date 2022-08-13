namespace PcStatsReporter.Core.Models;

public interface ICollector<T> where T : Sample
{
    T Collect();
}