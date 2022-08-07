namespace PcStatsReporter.Client.Initialization;

// todo: use cancellation token
public interface IInitializable
{
    void Initialize();
    Task WaitForInitialization();
    bool IsInitialized();
}