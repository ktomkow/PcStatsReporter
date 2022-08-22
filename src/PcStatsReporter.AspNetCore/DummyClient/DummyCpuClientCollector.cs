using System;
using System.Collections.Generic;
using PcStatsReporter.Core.Models;

namespace PcStatsReporter.AspNetCore.DummyClient;

public class DummyCpuClientCollector : ICollector<CpuSample>
{
    private readonly Random _rand;
    private readonly DummyClientSettings _settings;

    public DummyCpuClientCollector(DummyClientSettings settings)
    {
        _rand = new Random();
        _settings = settings;
    }

    public CpuSample Collect()
    {
        var sample = new CpuSample
        {
            Temperature = (uint) _rand.Next((int) _settings.MinCpuTemperature, (int) _settings.MaxCpuTemperature + 1),
            AverageLoad = (uint) _rand.Next(0, 101),
            Cores = new List<CoreSample>()
        };

        for (uint i = 0; i < _settings.CpuCores; i++)
        {
            var core = new CoreSample()
            {
                CoreNumber = i,
                Speed = (uint) _rand.Next((int) _settings.MinCpuSpeed, (int) _settings.MaxCpuSpeed + 1),
                Temperature = (uint) _rand.Next((int) _settings.MinCpuTemperature,
                    (int) _settings.MaxCpuTemperature + 1),
                ThreadsLoad = new List<(uint threadNumber, uint threadLoad)>()
                {
                    (1, (uint)_rand.Next(0,101)), // todo: to settings
                    (2, (uint)_rand.Next(0,101)) // todo: to settings
                }
            };

            sample.Cores.Add(core);
        }

        return sample;
    }
}