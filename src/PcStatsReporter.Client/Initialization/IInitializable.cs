namespace PcStatsReporter.Client.Initialization;

public interface IInitializable
{
    Task WaitForInitialization();
    Task<bool> IsInitialized();
}