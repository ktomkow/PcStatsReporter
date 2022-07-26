using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using PcStatsReporter.Grpc.Proto;
using PcStatsReporter.Grpc.Services;
using Rebus.Bus;

namespace PcStatsReporter.AspNetCore.Grpc;

public class GrpcCollectorService : CollectorService
{
    private readonly ILogger<GrpcCollectorService> _logger;
    private readonly IBus _bus;

    public GrpcCollectorService(ILogger<GrpcCollectorService> logger, IBus bus)
    {
        _logger = logger;
        _bus = bus;
    }
    
    public override async Task<DataResponse> Collect(CollectedData request, ServerCallContext context)
    {
        var temperature = request.Cpu.Temperature;
        _logger.LogInformation("Got request {Id}, Temperature: {Temperature} C", request.Uuid.Value, temperature);

        var response = new DataResponse()
        {
            Success = true
        };

        return await Task.FromResult(response);
    }
}