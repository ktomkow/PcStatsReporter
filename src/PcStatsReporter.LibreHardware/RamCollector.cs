using System;
using System.Linq;
using LibreHardwareMonitor.Hardware;
using PcStatsReporter.Core.Models;

namespace PcStatsReporter.LibreHardware;

public class RamCollector : ICollector<RamSample>
{
    private readonly Computer _computer;
    
    public RamCollector()
    {
        _computer = new Computer
        {
            IsMemoryEnabled = true
        };

        _computer.Open();
    }
    
    public RamSample Collect()
    {
        foreach (var hardware in _computer.Hardware)
        {
            hardware.Update();

            // just to be sure
            if (hardware.HardwareType != HardwareType.Memory)
            {
                continue;
            }

            var ramSensors = hardware.Sensors
                .Where(x => x.SensorType == SensorType.Data)
                .Where(x => x.Value.HasValue)
                .ToList();

            var usedSensor = ramSensors.FirstOrDefault(x =>
                x.Name.Contains("memory used", StringComparison.InvariantCultureIgnoreCase));

            var usedRam = usedSensor.Value;

            RamSample ram = new RamSample()
            {
                InUse = (double) usedRam
            };

            return ram;
            
        }

        throw new Exception("No data found");
    }
}