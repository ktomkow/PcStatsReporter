namespace PcStatsReporter.AspNetCore.Configuration;

public interface IConfigPrinter
{
    /// <summary>
    /// Print configuration data from IConfiguration
    /// </summary>
    void Print();
}