using System.Threading.Tasks;

namespace PcStatsReporter.Core.Initialization;

// todo: use cancellation token
public interface IInitializable
{
    Task Initialize();
    Task WaitForInitialization();
    bool IsInitialized();
}