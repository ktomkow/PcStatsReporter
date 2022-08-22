using System;
using PcStatsReporter.Core.Models;

namespace PcStatsReporter.AspNetCore.DummyClient;

public class DummyGpuClientCollector : ICollector<GpuSample>
{
    private readonly Random _rand;
    private readonly DummyClientSettings _settings;

    public DummyGpuClientCollector(DummyClientSettings settings)
    {
        _rand = new Random();
        _settings = settings;
    }
    
    public GpuSample Collect()
    {
        return new GpuSample()
        {
            CoreTemperature =
                (uint) _rand.Next((int) _settings.MinGpuTemperature, (int) _settings.MaxGpuTemperature + 1),
            GpuCoreLoad = 0,
        };
    }
}