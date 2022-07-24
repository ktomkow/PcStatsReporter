namespace PcStatsReporter.Client;

public interface IInitializer<T> where T : IInitializable
{
    Task Initialize(T initializable);
}