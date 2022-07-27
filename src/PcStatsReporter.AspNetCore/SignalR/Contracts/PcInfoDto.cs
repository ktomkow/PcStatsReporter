namespace PcStatsReporter.AspNetCore.SignalR.Contracts;

public class PcInfoDto
{
    public string CpuName { get; set; }
    public string GpuName { get; set; }
    public double TotalRam { get; set; }
}