using System.Threading.Tasks;

namespace PcStatsReporter.Core.Persistence
{
    public interface IHold
    {
        Task Set<T>(T obj);
        Task<T> Get<T>();
    }
}