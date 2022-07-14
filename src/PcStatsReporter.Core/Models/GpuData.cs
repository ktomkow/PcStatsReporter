namespace PcStatsReporter.Core.Models
{
    public class GpuData
    {
        public string Name { get; set; }
        public uint Temperature { get; set; }
        public uint LoadCore { get; set; }
    }
}