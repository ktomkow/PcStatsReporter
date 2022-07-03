namespace PcStatsReporter.RestContracts;

public class CpuCoreResponse
{
    public uint Id { get; set; }
    public uint Temperature { get; set; }
    public uint Speed { get; set; }
    public uint Load { get; set; }
}