using System;
using LibreHardwareMonitor.Hardware;
using PcStatsReporter.Core.Models;

namespace PcStatsReporter.LibreHardware;

public class GpuDataCollector
{
    private readonly Computer computer;
    
    public GpuDataCollector()
    {
        this.computer = new Computer
        {
            IsMemoryEnabled = true
        };

        this.computer.Open();
    }
    
    public GpuData Collect()
    {
        foreach (var hardware in computer.Hardware)
        {
            hardware.Update(); //use hardware.Name to get CPU model

            var data = hardware.Sensors.GetRam();
            
            GpuData gpu = new GpuData()
            {
                // Used = Math.Round(data.used, 2),
                // Total = Math.Round(data.used + data.available, 2)
            };

            return gpu;
        }

        return null;
    }
}