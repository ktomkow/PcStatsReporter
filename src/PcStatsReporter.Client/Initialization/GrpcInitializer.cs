﻿using Grpc.Net.Client;
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
    private readonly IServiceFinder _scanner;

    private readonly int apiPort = 11111;
    private readonly int grpcPort = 22222;

    public GrpcInitializer(ILogger<GrpcInitializer> logger, AppContext appContext, IBus bus, IServiceFinder scanner)
    {
        _logger = logger;
        _appContext = appContext;
        _bus = bus;
        _scanner = scanner;
    }

    public async Task Handle(GrpcInitializeCommand message)
    {
        _logger.LogInformation("Initialize grpc client started");
        var grpcChannel = await CreateGrpcChannel();

        _appContext.SetChannel(grpcChannel);

        var @event = new GrpcInitializedEvent();
        await _bus.Publish(@event);

        _logger.LogInformation("Initialize grpc client finished");
    }

    private async Task<GrpcChannel> CreateGrpcChannel()
    {
        _logger.LogInformation("Start scanning network to find server");
        string host = await _scanner.FindService(apiPort);
        _logger.LogInformation("Server host: {Host}", host);

        GrpcChannel grpcChannel = GrpcChannel.ForAddress(host + ":" + grpcPort);
        return grpcChannel;
    }
}