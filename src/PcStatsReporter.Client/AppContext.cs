using PcStatsReporter.Core.ReportingClientSettings;

namespace PcStatsReporter.Client;

public class AppContext : Initializable
{
    public ClientChannel ClientChannel { get; }
    public Settings Settings { get; }


    public AppContext(
        ClientChannel clientChannel, 
        Settings settings)
    {
        ClientChannel = clientChannel;
        Settings = settings;
    }

    public override async Task<bool> IsInitialized()
    {
        return await ClientChannel.IsInitialized() && await Settings.IsInitialized();
    }
}