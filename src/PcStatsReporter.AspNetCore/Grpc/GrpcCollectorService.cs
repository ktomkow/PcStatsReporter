using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using PcStatsReporter.AspNetCore.Messages;
using PcStatsReporter.Core.Maps;
using PcStatsReporter.Core.Models;
using PcStatsReporter.Grpc.Proto;
using PcStatsReporter.Grpc.Services;
using Rebus.Bus;

namespace PcStatsReporter.AspNetCore.Grpc;

public class GrpcCollectorService : CollectorService
{
    private readonly ILogger<GrpcCollectorService> _logger;
    private readonly IBus _bus;
    private readonly IMap<CollectedData, CpuSample> _cpuMap;

    public GrpcCollectorService(ILogger<GrpcCollectorService> logger, IBus bus, IMap<CollectedData, CpuSample> cpuMap)
    {
        _logger = logger;
        _bus = bus;
        _cpuMap = cpuMap;
    }

    public override async Task<DataResponse> Collect(CollectedData request, ServerCallContext context)
    {
        var temperature = request.Cpu.Temperature;
        _logger.LogInformation("Got request {Id}, Temperature: {Temperature} C", request.Uuid.Value, temperature);

        if (request.DataCase == CollectedData.DataOneofCase.Cpu)
        {
            return await ProcessCpuSample(request);
        }

        var response = new DataResponse()
        {
            Success = true
        };

        return await Task.FromResult(response);
    }

    private async Task<DataResponse> ProcessCpuSample(CollectedData request)
    {
        var cpuSample = _cpuMap.Map(request);

        var @event = new CpuSampleArrived()
        {
            CpuSample = cpuSample
        };

        await _bus.Publish(@event);

        return new DataResponse()
        {
            Success = true
        };
    }
}