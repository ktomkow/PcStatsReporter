namespace PcStatsReporter.Client.Messages;

public class RegisterCommand
{
    public string CpuName { get; set; }
    public string GpuName { get; set; }
    public double TotalRam { get; set; }
}