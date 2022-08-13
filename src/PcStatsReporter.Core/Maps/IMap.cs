namespace PcStatsReporter.Core.Maps;

public interface IMap<TFrom, TTo>
{
    TTo Map(TFrom source);
}