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
        // _initializedObject = GrpcChannel.ForAddress("http://localhost:11111");
        Initialized = new ClientChannel();
        
        // TODO: call register method
        // TODO: get cpu name, gpu name, total ram
        await Task.Delay(TimeSpan.FromSeconds(6));
        initializable.MarkAsInitialized();
    }
}