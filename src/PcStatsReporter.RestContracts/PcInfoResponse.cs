namespace PcStatsReporter.RestContracts;

public class PcInfoResponse
{
    public string CpuName { get; set; }
    public string GpuName { get; set; }
    public double TotalRam { get; set; }
}