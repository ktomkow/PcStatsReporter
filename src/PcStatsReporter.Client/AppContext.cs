using Grpc.Net.Client;
using PcStatsReporter.Client.Initialization;

namespace PcStatsReporter.Client;

public class AppContext : Initializable
{
    private bool _isInitialized;
    public GrpcChannel ClientChannel { get; private set; }
    public Settings Settings { get; private set; }
    
    public void SetChannel(GrpcChannel channel)
    {
        if (_isInitialized == false)
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
        if (_isInitialized == false)
        {
            Settings = settings;
        }
        else
        {
            throw new Exception("AppContext already initialized!");
        }
    }

    public override async Task Initialize()
    {
        if (Settings is null)
        {
            throw new Exception($"{nameof(Settings)} Not set yet!");
        }

        if (ClientChannel is null)
        {
            throw new Exception($"{nameof(ClientChannel)} Not set yet!");
        }
        
        _isInitialized = true;
        await Task.CompletedTask;
    }

    public override async Task<bool> IsInitialized()
    {
        return await Task.FromResult(_isInitialized);
    }
}