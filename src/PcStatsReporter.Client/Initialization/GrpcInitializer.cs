using Grpc.Net.Client;
using Microsoft.Extensions.Logging;
using PcStatsReporter.Client.Messages;
using Rebus.Bus;
using Rebus.Handlers;

namespace PcStatsReporter.Client.Initialization;

public class GrpcInitializer : IHandleMessages<GrpcInitializeCommand>
{
    private readonly AppContext _appContext;
    private readonly IBus _bus;

    public GrpcInitializer(ILogger<GrpcInitializer> logger, AppContext appContext, IBus bus)
    {
        _appContext = appContext;
        _bus = bus;
    }
    
    public async Task Handle(GrpcInitializeCommand message)
    {
        // TODO: initialize here
        // TODO: find address of backend
        var grpcChannel = GrpcChannel.ForAddress("http://localhost:22222");
        
        // TODO: call register method
        // TODO: get cpu name, gpu name, total ram
        _appContext.SetChannel(grpcChannel);

        var @event = new GrpcInitializedEvent();
        await _bus.Publish(@event);
    }
}