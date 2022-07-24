using Grpc.Net.Client;
using PcStatsReporter.Client.Initialization;

namespace PcStatsReporter.Client;

public class ClientChannel : Initializable
{
    public GrpcChannel GrpcChannel { get; protected set; }

    public void SetChannel(GrpcChannel channel)
    {
        GrpcChannel = channel;
    }
    
    public override async Task<bool> IsInitialized()
    {
        return await Task.FromResult(GrpcChannel is not null);
    }
}