namespace PcStatsReporter.RestContracts;

public class CpuResponse
{
    public string Name { get; set; }
    public uint PackageTemperature { get; set; }
    public ICollection<CpuCoreResponse> Cores { get; set; }
}