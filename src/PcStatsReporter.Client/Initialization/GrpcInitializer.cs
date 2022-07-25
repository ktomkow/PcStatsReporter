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

    private readonly int apiPort = 11111;
    private readonly int grpcPort = 22222;

    public GrpcInitializer(ILogger<GrpcInitializer> logger, AppContext appContext, IBus bus, Scanner scanner)
    {
        _logger = logger;
        _appContext = appContext;
        _bus = bus;
        _scanner = scanner;
    }

    public async Task Handle(GrpcInitializeCommand message)
    {
        var grpcChannel = await CreateGrpcChannel();

        _appContext.SetChannel(grpcChannel);

        var @event = new GrpcInitializedEvent();
        await _bus.Publish(@event);
    }

    private async Task<GrpcChannel> CreateGrpcChannel()
    {
        string host = await _scanner.Scan(apiPort);
        GrpcChannel grpcChannel = GrpcChannel.ForAddress(host + ":" + grpcPort);
        return grpcChannel;
    }
}