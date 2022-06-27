using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using OpenHardwareMonitor.Hardware;

namespace PcStatsReporter.OpenHardware
{
    internal static class Program
    {
        public static async Task Main(string[] args)
        {

            Console.WriteLine("Init");

            var collector = new CpuDataCollector(null);
            await collector.Start();

            Console.WriteLine("Finished");
            Console.ReadLine();


        }
    }
}