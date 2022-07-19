namespace PcStatsReporter.RestContracts;

public class GpuResponse
{
    public string Name { get; set; }
    public uint Temperature { get; set; }
    public uint LoadCore { get; set; }
}