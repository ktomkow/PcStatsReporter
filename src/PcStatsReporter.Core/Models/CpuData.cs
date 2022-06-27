using System.Collections.Generic;

namespace PcStatsReporter.Core.Models
{
    public class CpuData
    {
        public ICollection<CpuCore> Cores { get; set; }

        public uint PackageTemperature { get; set; }

        public double PackageLoad { get; set; }

        public CpuData()
        {
            this.Cores = new List<CpuCore>();
        }
    }
}