namespace PcStatsReporter.Client;

public interface IInitializable
{
    Task WaitForInitialization();
    Task<bool> IsInitialized();
}