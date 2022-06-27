using PcStatsReporter.Proto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using PcStatsReporter.Core;
using PcStatsReporter.OpenHardware;

namespace PcStatsReporter.Server
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            Console.WriteLine("Init Server");

            var tasks = new List<Task>();

            Store store = new Store();
            CpuDataCollector cpuDataCollector = new CpuDataCollector(store);
            tasks.Add(cpuDataCollector.Start());

            TcpListener server = new TcpListener(IPAddress.Parse("127.0.0.1"), 9090);
            server.Start();
            Console.WriteLine("Server has started on 127.0.0.1:9090.{0}Waiting for a connection...",
                Environment.NewLine);

            while (true)
            {
                TcpClient client = await server.AcceptTcpClientAsync();
                Channel channel = new Channel(client, store);
                var task = channel.Run();
                tasks.Add(task);
            }

            Console.WriteLine("Finished");
            Console.ReadLine();
        }
    }
}