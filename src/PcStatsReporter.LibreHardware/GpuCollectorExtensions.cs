using System;
using System.Collections.Generic;
using System.Linq;
using LibreHardwareMonitor.Hardware;
using PcStatsReporter.Core.Models;

namespace PcStatsReporter.LibreHardware;

public static class GpuCollectorExtensions
{
    public static GpuData GetGpu(this IEnumerable<IHardware> hardware)
    {
        var gpu = hardware.FirstOrDefault(x => x.HardwareType is HardwareType.GpuNvidia);
        
        var temperatureSensor = gpu.Sensors
            .Where(x => x.Value.HasValue)
            .Where(x => x.SensorType == SensorType.Temperature)
            .First(x => x.Name.Contains("gpu core", StringComparison.InvariantCultureIgnoreCase));
        
        var loadSensor = gpu.Sensors
            .Where(x => x.Value.HasValue)
            .Where(x => x.SensorType == SensorType.Load)
            .First(x => x.Name.Contains("gpu core", StringComparison.InvariantCultureIgnoreCase));

        var result = new GpuData()
        {
            Name = gpu.Name,
            Temperature = (uint) temperatureSensor.Value,
            LoadCore = (uint) loadSensor.Value
        };
        
        return result;
    }
}