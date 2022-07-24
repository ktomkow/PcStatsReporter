namespace PcStatsReporter.Client.Initialization;

public interface IInitializer<T> where T : IInitializable
{
    Task Initialize(T initializable);
}