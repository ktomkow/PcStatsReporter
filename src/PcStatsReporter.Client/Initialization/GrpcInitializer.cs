using Grpc.Net.Client;
using Microsoft.Extensions.Logging;
using PcStatsReporter.Client.Messages;
using PcStatsReporter.Client.NetworkScanner;
using Rebus.Bus;
using Rebus.Handlers;

namespace PcStatsReporter.Client.Initialization;

public class GrpcInitializer : IHandleMessages<GrpcInitializeCommand>
{
    private readonly ILogger<GrpcInitializer> _logger;
    private readonly AppContext _appContext;
    private readonly IBus _bus;
    private readonly Scanner _scanner;

    public GrpcInitializer(ILogger<GrpcInitializer> logger, AppContext appContext, IBus bus, Scanner scanner)
    {
        _logger = logger;
        _appContext = appContext;
        _bus = bus;
        _scanner = scanner;
    }
    
    public async Task Handle(GrpcInitializeCommand message)
    {
        _logger.LogInformation("Service {Service} Initializing", this.GetType().Name);
        string host = await _scanner.Scan(11111);
        // TODO: initialize here
        // TODO: find address of backend
        var grpcChannel = GrpcChannel.ForAddress("http://localhost:22222");
        
        // TODO: call register method
        // TODO: get cpu name, gpu name, total ram
        _appContext.SetChannel(grpcChannel);

        _logger.LogInformation("Service {Service} Initialized", this.GetType().Name);

        var @event = new GrpcInitializedEvent();
        await _bus.Publish(@event);
    }
}