namespace PcStatsReporter.Client.Initialization;

public interface IInitializable
{
    Task Initialize();
    Task WaitForInitialization();
    Task<bool> IsInitialized();
}