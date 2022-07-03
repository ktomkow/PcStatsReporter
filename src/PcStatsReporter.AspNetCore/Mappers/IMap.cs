namespace PcStatsReporter.AspNetCore.Mappers;

public interface IMap<TFrom,TTo>
{
    TTo Map(TFrom source);
}