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
            IsGpuEnabled = true
        };

        this.computer.Open();
    }
    
    public GpuData Collect()
    {
        GpuData gpu = computer.Hardware.GetGpu();

        return gpu;
    }
}