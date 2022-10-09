using System;

namespace PcStatsReporter.AspNetCore.DummyClient;

public class DummyClientSettings
{
    public string CpuName { get; }
    public string GpuName { get; }
    public double TotalRam { get; } // GB
    public double MinRamUsage { get; } // GB
    public double MaxRamChange { get; } // GB

    public uint CpuCores { get; }
    public uint CpuCoreThreads { get; } // Threads per core
    public uint MinCpuSpeed { get; } // MHz
    public uint MaxCpuSpeed { get; } // MHz
    
    public uint MinCpuTemperature { get; } // Celsius
    public uint MaxCpuTemperature { get; } // Celsius
           
    public uint MinGpuTemperature { get; } // Celsius
    public uint MaxGpuTemperature { get; } // Celsius

    public TimeSpan CollectPeriod { get; set; }
    public TimeSpan HoldTime { get; set; }

    public DummyClientSettings()
    {
        CpuName = "Binemd x6-100y slow";
        GpuName = "AmVision XXX 1234!";
        TotalRam = 15.74d;
        MinRamUsage = 1.1d;
        MaxRamChange = 0.3d;
        CpuCores = 8;
        CpuCoreThreads = 2;
        MinCpuSpeed = 700;
        MaxCpuSpeed = 5600;
        MinCpuTemperature = 40;
        MaxCpuTemperature = 110;
        MinGpuTemperature = 40;
        MaxGpuTemperature = 110;

        CollectPeriod = TimeSpan.FromSeconds(1);
        HoldTime = TimeSpan.FromSeconds(5);
    }
}