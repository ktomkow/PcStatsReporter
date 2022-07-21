using System.Collections.Generic;
using System.Text;

namespace PcStatsReporter.Core.Models
{
    public class CoreSample
    {
        public uint CoreNumber { get; set; }
        public uint Temperature { get; set; }
        public uint Speed { get; set; }
        public IReadOnlyList<(uint threadNumber, uint threadLoad)> ThreadsLoad { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            
            return sb.ToString();
        }
    }
}