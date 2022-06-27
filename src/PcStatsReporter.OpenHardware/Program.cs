using System;
using System.Threading.Tasks;
using PcStatsReporter.Core;

namespace PcStatsReporter.OpenHardware
{
    internal static class Program
    {
        public static async Task Main(string[] args)
        {

            Console.WriteLine("Init");

            var store = new Store();

            var collector = new CpuDataCollector(store);
            await collector.Start();

            Console.WriteLine("Finished");
            Console.ReadLine();


        }
    }
}