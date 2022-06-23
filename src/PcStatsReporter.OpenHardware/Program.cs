using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using OpenHardwareMonitor.Hardware;
using PcStatsReporter.Proto;

namespace PcStatsReporter.OpenHardware
{
    internal static class Program
    {
        public static async Task Main(string[] args)
        {
            Console.WriteLine("Init");
            var computer = new Computer {CPUEnabled = true};
            computer.Open();

            var coreAndTemperature = new Dictionary<string, float>();

            foreach (var hardware in computer.Hardware)
            {
                hardware.Update(); //use hardware.Name to get CPU model
                foreach (var sensor in hardware.Sensors)
                {
                    Console.WriteLine(sensor.SensorType + " " + sensor.Value.HasValue);
                    if (sensor.SensorType == SensorType.Temperature && sensor.Value.HasValue)
                    {
                        coreAndTemperature.Add(sensor.Name, sensor.Value.Value);
                    }
                }
            }

            foreach (var keyValuePair in coreAndTemperature)
            {
                Console.WriteLine($"{keyValuePair.Key} : {keyValuePair.Value}");
            }

            try
            {
                computer.Close();
            }
            catch (Exception)
            {
                //ignore closing errors
            }
            
            var tasks = new List<Task>();
            TcpListener server = new TcpListener(IPAddress.Parse("127.0.0.1"), 9090);
            server.Start();
            Console.WriteLine("Server has started on 127.0.0.1:9090.{0}Waiting for a connection...",
                Environment.NewLine);

            var dupa = new Dupa();
            dupa.Id = 5;
            
            while (true)
            {
                TcpClient client = await server.AcceptTcpClientAsync();
                Channel channel = new Channel(client);
                var task = channel.Run();
                tasks.Add(task);
            }
            
            Console.WriteLine("Finished");
            Console.ReadLine();
        }
    }
}