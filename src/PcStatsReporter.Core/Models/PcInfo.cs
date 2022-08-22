namespace PcStatsReporter.Core.Models
{
    public class PcInfo : Sample
    {
        public string CpuName { get; set; }
        public string GpuName { get; set; }
        public double TotalRam { get; set; }
        
        protected override string Format()
        {
            return $"{base.ToString()}, {nameof(CpuName)}: {CpuName}, {nameof(GpuName)}: {GpuName}, {nameof(TotalRam)}: {TotalRam}";
        }
    }
}