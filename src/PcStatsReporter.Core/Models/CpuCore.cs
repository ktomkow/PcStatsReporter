using System;

namespace PcStatsReporter.Core.Models
{
    public class CpuCore
    {
        public uint Id { get; set; }
        public uint Temperature { get; set; }
        public uint Speed { get; set; }
        public uint Load { get; set; }

        public override string ToString()
        {
            return $"Id: {Id}, Temperature: {Temperature}, Speed: {Speed}, Load: {Load}";
        }
    }
}