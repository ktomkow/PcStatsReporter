using System.Threading.Tasks;

namespace PcStatsReporter.Core.Initializable;

// todo: use cancellation token
public interface IInitializable
{
    Task Initialize();
    Task WaitForInitialization();
    bool IsInitialized();
}