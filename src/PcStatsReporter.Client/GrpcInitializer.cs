using Grpc.Net.Client;
using Microsoft.Extensions.Logging;

namespace PcStatsReporter.Client;

public class GrpcInitializer : Initializer<ClientChannel>
{
    public GrpcInitializer(ILogger<GrpcInitializer> logger) : base(logger)
    {
    }

    protected override async Task InitializeResult(ClientChannel initializable)
    {
        // TODO: initialize here
        // TODO: find address of backend
        var grpcChannel = GrpcChannel.ForAddress("http://localhost:22222");
        
        // TODO: call register method
        // TODO: get cpu name, gpu name, total ram
        initializable.SetChannel(grpcChannel);
        await Task.CompletedTask;
    }
}