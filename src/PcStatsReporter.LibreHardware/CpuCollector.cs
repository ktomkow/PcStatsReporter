using System;
using System.Collections.Generic;
using System.Linq;
using LibreHardwareMonitor.Hardware;
using PcStatsReporter.Core.Models;

namespace PcStatsReporter.LibreHardware;

public class CpuCollector : ICollector<CpuSample>
{
    private readonly Computer _computer;

    public CpuCollector()
    {
        _computer = new Computer
        {
            IsCpuEnabled = true
        };

        _computer.Open();
    }

    public CpuSample Collect()
    {
        foreach (var hardware in _computer.Hardware)
        {
            hardware.Update();

            // just to be sure
            if (hardware.HardwareType != HardwareType.Cpu)
            {
                continue;
            }

            var sensors = hardware.Sensors.ToList();
            CpuSample cpu = new CpuSample()
            {
                Temperature = GetPackageTemperature(sensors),
                AverageLoad = GetAverageLoad(sensors),
                Cores = GetCpuCores(sensors)
            };
            
            return cpu;
        }

        throw new Exception("No data found");
    }

    private static uint GetPackageTemperature(IEnumerable<ISensor> sensors)
    {
        ISensor? sensor = sensors
            .Where(x => x.SensorType == SensorType.Temperature)
            .Where(x => x.Value.HasValue)
            .FirstOrDefault(x => x.Name.Contains("cpu package", StringComparison.InvariantCultureIgnoreCase));

        if (sensor is null)
        {
            return default;
        }

        return (uint) sensor.Value;
    }

    private static uint GetAverageLoad(IEnumerable<ISensor> sensors)
    {
        ISensor? sensor = sensors
            .Where(x => x.SensorType == SensorType.Load)
            .Where(x => x.Value.HasValue)
            .FirstOrDefault(x => x.Name.Contains("cpu total", StringComparison.InvariantCultureIgnoreCase));

        if (sensor is null)
        {
            return default;
        }

        return (uint) sensor.Value;
    }

    private static List<CoreSample> GetCpuCores(IEnumerable<ISensor> sensors)
    {
        Dictionary<uint, CoreSample> cores = new Dictionary<uint, CoreSample>();

        foreach (var sensor in sensors)
        {
            if (sensor.Value.HasValue == false)
            {
                continue;
            }

            bool isCoreInfo = sensor.Name.TryGetCoreId(out uint coreId);
            if (isCoreInfo == false)
            {
                continue;
            }

            if (cores.ContainsKey(coreId) == false)
            {
                CoreSample newCore = new CoreSample() {CoreNumber = coreId};
                cores.Add(coreId, newCore);
            }

            CoreSample core = cores[coreId];

            switch (sensor.SensorType)
            {
                case SensorType.Temperature:
                    core.Temperature = (uint) sensor.Value;
                    break;

                case SensorType.Clock:
                    core.Speed = (uint) sensor.Value;
                    break;

                case SensorType.Load:
                    uint currentMax = 0;
                    if (core.ThreadsLoad.Any())
                    {
                        currentMax = core.ThreadsLoad.Max(x => x.threadNumber);
                    }
                    core.ThreadsLoad.Add((++currentMax, (uint) sensor.Value));
                    break;
            }
        }

        return cores.Values.ToList();
    }
}