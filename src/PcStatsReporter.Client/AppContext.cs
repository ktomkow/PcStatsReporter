using Grpc.Net.Client;
using PcStatsReporter.Core.Initializable;

namespace PcStatsReporter.Client;

public class AppContext : Initializable
{
    public GrpcChannel ClientChannel { get; private set; }
    public Settings Settings { get; private set; }
    
    public void SetChannel(GrpcChannel channel)
    {
        if (this.IsInitialized() == false)
        {
            ClientChannel = channel;
        }
        else
        {
            throw new Exception("AppContext already initialized!");
        }
    }

    public void SetSettings(Settings settings)
    {
        if (this.IsInitialized() == false)
        {
            Settings = settings;
        }
        else
        {
            throw new Exception("AppContext already initialized!");
        }
    }

    public override Task Initialize()
    {
        if (Settings is null)
        {
            throw new Exception($"{nameof(Settings)} Not set yet!");
        }

        if (ClientChannel is null)
        {
            throw new Exception($"{nameof(ClientChannel)} Not set yet!");
        }

        this.SetInitialized();
        return Task.CompletedTask;
    }
}