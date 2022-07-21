namespace PcStatsReporter.Core.Models
{
    public class RamSample : Sample
    {
        public double InUse { get; set; }
        protected override string Format()
        {
            return $"InUse: {InUse} GB";
        }
    }
}