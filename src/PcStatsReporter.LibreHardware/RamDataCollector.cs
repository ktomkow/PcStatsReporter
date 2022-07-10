using System;
using LibreHardwareMonitor.Hardware;
using PcStatsReporter.Core.Models;

namespace PcStatsReporter.LibreHardware;

public class RamDataCollector
{
    private readonly Computer computer;
    
    public RamDataCollector()
    {
        this.computer = new Computer
        {
            IsMemoryEnabled = true
        };

        this.computer.Open();
    }
    
    public RamData Collect()
    {
        foreach (var hardware in computer.Hardware)
        {
            hardware.Update(); //use hardware.Name to get CPU model

            var data = hardware.Sensors.GetRam();
            
            RamData ram = new RamData()
            {
                Used = Math.Round(data.used, 2),
                Total = Math.Round(data.used + data.available, 2)
            };

            return ram;
        }

        throw new Exception("No data found");
    }
}