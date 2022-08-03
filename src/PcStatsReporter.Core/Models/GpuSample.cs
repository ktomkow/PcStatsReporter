using System.Text;

namespace PcStatsReporter.Core.Models
{
    public class GpuSample : Sample
    {
        public uint CoreTemperature { get; set; }
        public uint GpuCoreClock { get; set; }
        public uint GpuMemoryClock { get; set; }
        public uint GpuCoreLoad { get; set; }
        public uint GpuMemoryControllerLoad { get; set; }
        public uint GpuVideEngineLoad { get; set; }
        public uint GpuBusLoad { get; set; }
        public uint GpuMemoryUsed { get; set; }
        
        protected override string Format()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"CoreTemperature: {CoreTemperature} C");
            
            sb.AppendLine($"GpuCoreClock: {GpuCoreClock} MHz");
            sb.AppendLine($"GpuMemoryClock: {GpuMemoryClock} MHz");
            
            sb.AppendLine($"GpuCoreLoad: {GpuCoreLoad} %");
            sb.AppendLine($"GpuMemoryControllerLoad: {GpuMemoryControllerLoad} %");
            sb.AppendLine($"GpuVideEngineLoad: {GpuVideEngineLoad} %");
            sb.AppendLine($"GpuBusLoad: {GpuBusLoad} %");
            
            sb.AppendLine($"GpuMemoryUsed: {GpuMemoryUsed} C");

            return sb.ToString();
        }
    }
}