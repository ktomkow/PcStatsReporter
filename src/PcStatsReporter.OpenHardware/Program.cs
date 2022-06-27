using System;
using System.Threading.Tasks;
using PcStatsReporter.Core;
using PcStatsReporter.Core.Models;

namespace PcStatsReporter.OpenHardware
{
    internal static class Program
    {
        public static async Task Main(string[] args)
        {

            Console.WriteLine("Init");

            var store = new Store();

            var collector = new CpuDataCollector(store);
            collector.Start();
            
            while (true)
            {
                await Task.Delay(TimeSpan.FromSeconds(1));
                Console.Clear();
                CpuData cpuData = store.Get<CpuData>();
                Console.WriteLine(cpuData);
            }

            Console.WriteLine("Finished");
            Console.ReadLine();


        }
    }
}