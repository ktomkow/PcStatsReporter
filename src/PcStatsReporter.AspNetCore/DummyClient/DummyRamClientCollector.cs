using System;
using PcStatsReporter.Core.Models;

namespace PcStatsReporter.AspNetCore.DummyClient;

public class DummyRamClientCollector : ICollector<RamSample>
{
    private readonly Random _rand;
    private readonly DummyClientSettings _settings;
    private double _currentRamUsage;

    public DummyRamClientCollector(DummyClientSettings settings)
    {
        _rand = new Random();
        _settings = settings;
        _currentRamUsage = settings.MinRamUsage;
    }
    
    public RamSample Collect()
    {
        var sample = new RamSample()
        {
            InUse = GetNext()
        };

        return sample;
    }

    private double GetNext()
    {
        var result = _currentRamUsage + _settings.MaxRamChange * _rand.Next(-1, 2) * _rand.NextDouble();

        if (result > _settings.TotalRam)
        {
            result = _settings.TotalRam;
        }

        if (result < _settings.MinRamUsage)
        {
            result = _settings.MinRamUsage;
        }

        _currentRamUsage = result;
        return result;
    }
}