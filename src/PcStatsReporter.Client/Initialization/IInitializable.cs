namespace PcStatsReporter.Client.Initialization;

// todo: use cancellation token
public interface IInitializable
{
    Task Initialize();
    Task WaitForInitialization();
    Task<bool> IsInitialized();
}