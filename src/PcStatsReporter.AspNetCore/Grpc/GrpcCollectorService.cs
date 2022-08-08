using System;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using PcStatsReporter.AspNetCore.Messages;
using PcStatsReporter.Core.Maps;
using PcStatsReporter.Core.Messages;
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
    private readonly IMap<CollectedData, GpuSample> _gpuMap;
    private readonly IMap<CollectedData, RamSample> _ramMap;

    public GrpcCollectorService(
        ILogger<GrpcCollectorService> logger,
        IBus bus,
        IMap<CollectedData, CpuSample> cpuMap,
        IMap<CollectedData, GpuSample> gpuMap,
        IMap<CollectedData, RamSample> ramMap)
    {
        _logger = logger;
        _bus = bus;
        _cpuMap = cpuMap;
        _gpuMap = gpuMap;
        _ramMap = ramMap;
    }

    public override async Task<DataResponse> Collect(CollectedData request, ServerCallContext context)
    {
        var temperature = request.Cpu.Temperature;
        
        _logger.LogInformation("Got request {Id}, Temperature: {Temperature} C", request.Uuid.Value, temperature);
        
        try
        {
            var @event = request.DataCase switch
            {
                CollectedData.DataOneofCase.None => throw new ArgumentNullException(nameof(request.DataCase)),
                CollectedData.DataOneofCase.Cpu => ProcessCpuSample(request),
                CollectedData.DataOneofCase.Gpu => ProcessGpuSample(request),
                CollectedData.DataOneofCase.Ram => ProcessRamSample(request),
                _ => throw new ArgumentOutOfRangeException()
            };

            await _bus.Publish(@event);
            
            var response = new DataResponse()
            {
                Success = true
            };

            return response;
        }
        catch (Exception e)
        {
            // todo: logging
            Console.WriteLine(e.Message);
            var response = new DataResponse()
            {
                Success = false
            };

            return response;
        }
    }

    private IEvent ProcessCpuSample(CollectedData request)
    {
        var cpuSample = _cpuMap.Map(request);

        var @event = new CpuSampleArrivedEvent()
        {
            CpuSample = cpuSample
        };

        return @event;
    }
    
    private IEvent ProcessGpuSample(CollectedData request)
    {
        var cpuSample = _cpuMap.Map(request);

        var @event = new CpuSampleArrivedEvent()
        {
            CpuSample = cpuSample
        };

        return @event;
    }
    
    private IEvent ProcessRamSample(CollectedData request)
    {
        var cpuSample = _cpuMap.Map(request);

        var @event = new CpuSampleArrivedEvent()
        {
            CpuSample = cpuSample
        };

        return @event;
    }
}