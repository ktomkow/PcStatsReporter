using System;
using System.Collections.Generic;
using System.Linq;
using LibreHardwareMonitor.Hardware;
using PcStatsReporter.Core.Models;

namespace PcStatsReporter.LibreHardware;

public class GpuCollector : ICollector<GpuSample>
{
    private readonly Computer _computer;

    public GpuCollector()
    {
        _computer = new Computer
        {
            IsGpuEnabled = true
        };

        _computer.Open();
    }

    public GpuSample Collect()
    {
        foreach (var hardware in _computer.Hardware)
        {
            hardware.Update();

            // just to be sure
            if (hardware.HardwareType != HardwareType.GpuNvidia && hardware.HardwareType != HardwareType.GpuAmd)
            {
                continue;
            }

            var sensors = hardware.Sensors.ToList();
            GpuSample cpu = new GpuSample()
            {
                CoreTemperature = GetValue(sensors, SensorType.Temperature, "gpu core"),
                GpuCoreClock = GetValue(sensors, SensorType.Clock, "gpu core"),
                GpuMemoryClock = GetValue(sensors, SensorType.Clock, "gpu memory"),
                GpuCoreLoad = GetValue(sensors, SensorType.Load, "gpu core"),
                GpuMemoryControllerLoad = GetValue(sensors, SensorType.Load, "gpu memory controller"),
                GpuVideEngineLoad = GetValue(sensors, SensorType.Load, "gpu video engine"),
                GpuBusLoad = GetValue(sensors, SensorType.Load, "gpu bus"),
                GpuMemoryUsed = GetValue(sensors, SensorType.SmallData, "gpu memory used")
            };

            return cpu;
        }

        throw new Exception("No data found");
    }

    private uint GetValue(IList<ISensor> sensors, SensorType type, string field)
    {
        ISensor? sensor = sensors
            .Where(x => x.Value.HasValue)
            .Where(x => x.SensorType == type)
            .FirstOrDefault(x => x.Name.Contains(field, StringComparison.InvariantCultureIgnoreCase));

        if (sensor?.Value is null)
        {
            return default(uint);
        }

        return (uint) sensor.Value;
    }
}