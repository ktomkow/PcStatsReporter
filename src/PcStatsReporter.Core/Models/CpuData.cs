using System.Collections.Generic;
using System.Text;

namespace PcStatsReporter.Core.Models
{
    public class CpuData
    {
        public string Name { get; set; }
        public ICollection<CpuCore> Cores { get; set; }
        
        public CpuData()
        {
            this.Cores = new List<CpuCore>();
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine(Name);
            
            foreach (var core in Cores)
            {
                sb.AppendLine($"Id: {core.Id}, Temperature: {core.Temperature}, Speed: {core.Speed}, Load: {core.Load}");
            }
            
            return sb.ToString();
        }
    }
}