using System;
using System.Collections.Generic;

namespace PcStatsReporter.Core.Models
{
    public class CpuCore
    {
        public uint Id { get; set; }
        public uint Temperature { get; set; }
        public uint Speed { get; set; }
        public ICollection<uint> Load { get; set; } = new List<uint>();
        
        public override string ToString()
        {
            return $"Id: {Id}, Temperature: {Temperature}, Speed: {Speed}, Load: {string.Join(", ", Load)}";
        }
    }
}