using Grpc.Net.Client;

namespace PcStatsReporter.Client;

public class ClientChannel : Initializable
{
    private bool _isInitialized;
    public GrpcChannel GrpcChannel { get; protected set; }
    
    public void MarkAsInitialized()
    {
        _isInitialized = true;
    }
    
    public override async Task<bool> IsInitialized()
    {
        return await Task.FromResult(_isInitialized);
    }
}