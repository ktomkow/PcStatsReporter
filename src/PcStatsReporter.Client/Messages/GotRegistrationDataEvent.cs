namespace PcStatsReporter.Client.Messages;

public class GotRegistrationDataEvent
{
    public string CpuName { get; set; }
    public string GpuName { get; set; }
    public double TotalRam { get; set; }
}