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
        int apiPort = 11111;
        int grpcPort = 22222;
        
        string host = await _scanner.Scan(apiPort);
        var grpcChannel = GrpcChannel.ForAddress(host + ":" + grpcPort);
        
        // TODO: call register method
        // TODO: get cpu name, gpu name, total ram
        _appContext.SetChannel(grpcChannel);

        _logger.LogInformation("Service {Service} Initialized", this.GetType().Name);

        var @event = new GrpcInitializedEvent();
        await _bus.Publish(@event);
    }
}