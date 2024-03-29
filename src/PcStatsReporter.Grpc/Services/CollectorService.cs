﻿using System;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using PcStatsReporter.Core.Maps;
using PcStatsReporter.Core.Messages;
using PcStatsReporter.Core.Models;
using PcStatsReporter.Grpc.Proto;
using Rebus.Bus;

namespace PcStatsReporter.Grpc.Services;

public class CollectorService : Collector.CollectorBase
{
    private readonly ILogger<CollectorService> _logger;
    private readonly IBus _bus;
    private readonly IMap<CollectedData, CpuSample> _cpuMap;
    private readonly IMap<CollectedData, GpuSample> _gpuMap;
    private readonly IMap<CollectedData, RamSample> _ramMap;

    public CollectorService(
        ILogger<CollectorService> logger,
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
        try
        {
            _logger.LogTrace("Handling collected data of type {Type}", request.DataCase);
            
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
            _logger.LogError(e, "Error during handling collect attempt of data case {DataCase}", request.DataCase);
            
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
        var gpuSample = _gpuMap.Map(request);

        var @event = new GpuSampleArrivedEvent()
        {
            GpuSample = gpuSample
        };

        return @event;
    }
    
    private IEvent ProcessRamSample(CollectedData request)
    {
        var ramSample = _ramMap.Map(request);

        var @event = new RamSampleArrivedEvent()
        {
            RamSample = ramSample
        };

        return @event;
    }
}