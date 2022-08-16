namespace PcStatsReporter.Client.NetworkScanner;

public interface IServiceFinder
{
    Task<string> FindService(int port);
}