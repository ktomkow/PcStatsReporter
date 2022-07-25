using System.Collections.Generic;
using System.Text;

namespace PcStatsReporter.Core.Models
{
    public class CpuSample : Sample
    {
        public uint Temperature { get; set; }
        public uint AverageLoad { get; set; }
        public IList<CoreSample> Cores { get; set; } = new List<CoreSample>();
        protected override string Format()
        {
            var sb = new StringBuilder();

            sb.AppendLine($"Temperature: {Temperature} C");
            sb.AppendLine($"AverageLoad: {AverageLoad} %");
            foreach (var core in Cores)
            {
                sb.AppendLine($"Core #{core.CoreNumber}");
                sb.AppendLine($"  Temperature {core.Temperature} C");
                sb.AppendLine($"  Speed {core.Speed} MHz");
                foreach (var thread in core.ThreadsLoad)
                {
                    sb.AppendLine($"    Thread #{thread.threadNumber}, Load: {thread.threadLoad} %");
                }
            }
            
            return sb.ToString();
        }
    }
}