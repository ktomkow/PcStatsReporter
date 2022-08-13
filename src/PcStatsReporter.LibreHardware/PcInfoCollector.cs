using System;
using System.Linq;
using LibreHardwareMonitor.Hardware;
using PcStatsReporter.Core.Models;

namespace PcStatsReporter.LibreHardware;

public class PcInfoCollector
{
    private readonly Computer _computer;

    public PcInfoCollector()
    {
        _computer = new Computer
        {
            IsCpuEnabled = true,
            IsMemoryEnabled = true,
            IsGpuEnabled = true
        };
        
        _computer.Open();
    }

    public PcInfo Collect()
    {
        PcInfo result = new PcInfo();

        foreach (var hardware in _computer.Hardware)
        {
            hardware.Update();
            if (hardware.HardwareType == HardwareType.Cpu)
            {
                result.CpuName = GetCpuName(hardware);
            }
            else if (hardware.HardwareType == HardwareType.Memory)
            {
                result.TotalRam = GetTotalRam(hardware);
            }
            else if (hardware.HardwareType == HardwareType.GpuNvidia)
            {
                result.GpuName = GetGpuName(hardware);
            }
        }

        return result;
    }

    private string GetCpuName(IHardware hardware)
    {
        return hardware.Name;
    }
    
    private string GetGpuName(IHardware hardware)
    {
        return hardware.Name;
    }

    private double GetTotalRam(IHardware hardware)
    {
        var ramSensors = hardware.Sensors
            .Where(x => x.SensorType == SensorType.Data)
            .Where(x => x.Value.HasValue)
            .ToList();

        var usedSensor = ramSensors.FirstOrDefault(x =>
            x.Name.Contains("memory used", StringComparison.InvariantCultureIgnoreCase));

        var usedRam = usedSensor.Value;
                
        var availableSensor = ramSensors.FirstOrDefault(x =>
            x.Name.Contains("memory available", StringComparison.InvariantCultureIgnoreCase));

        var availableRam = availableSensor.Value;

        var totalRam = usedRam + availableRam;
        double result = (double) totalRam;

        return result;
    }
}