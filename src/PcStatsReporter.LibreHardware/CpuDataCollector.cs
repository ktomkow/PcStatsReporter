using System;
using LibreHardwareMonitor.Hardware;
using PcStatsReporter.Core.Models;

namespace PcStatsReporter.LibreHardware;

public class CpuDataCollector
{
    private readonly Computer computer;

    public CpuDataCollector()
    {
        this.computer = new Computer
        {
            IsCpuEnabled = true
        };

        this.computer.Open();
    }

    public CpuData Collect()
    {
        foreach (var hardware in computer.Hardware)
        {
            hardware.Update(); //use hardware.Name to get CPU model
            CpuData cpu = new CpuData
            {
                Name = hardware.Name,
                PackageTemperature = hardware.Sensors.GetPackageTemperature(),
                AverageLoad = hardware.Sensors.GetAverageLoad(),
                Cores = hardware.Sensors.GetCpuCores()
            };

            return cpu;
        }

        throw new Exception("No data found");
    }
}