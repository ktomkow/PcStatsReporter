namespace PcStatsReporter.AspNetCore.SignalR.Contracts;

public class GpuSampleDto
{
    public uint CoreTemperature { get; set; }
    public uint GpuCoreClock { get; set; }
    public uint GpuMemoryClock { get; set; }
    public uint GpuCoreLoad { get; set; }
    public uint GpuMemoryControllerLoad { get; set; }
    public uint GpuVideEngineLoad { get; set; }
    public uint GpuBusLoad { get; set; }
    public uint GpuMemoryUsed { get; set; }
}