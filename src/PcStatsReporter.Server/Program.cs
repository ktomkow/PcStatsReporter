using PcStatsReporter.Proto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace PcStatsReporter.Server
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {

            Console.WriteLine("Init Server");
            //var computer = new Computer { CPUEnabled = true };
            //computer.Open();

            //var coreAndTemperature = new Dictionary<string, float>();

            List<Core> cores = new List<Core>();

            //foreach (var hardware in computer.Hardware)
            //{
            //    hardware.Update(); //use hardware.Name to get CPU model
            //    uint i = 0;
            //    foreach (var sensor in hardware.Sensors)
            //    {
            //        Console.WriteLine(sensor.SensorType + " " + sensor.Value.HasValue);
            //        if (sensor.SensorType == SensorType.Temperature && sensor.Value.HasValue)
            //        {
            //            coreAndTemperature.Add(sensor.Name, sensor.Value.Value);
            //            var core = new Core()
            //            {
            //                Id = i,
            //                Speed = 0,
            //                Temperature = (uint)sensor.Value.Value
            //            };

            //            cores.Add(core);

            //            i++;
            //        }
            //    }
            //}

            //foreach (var keyValuePair in coreAndTemperature)
            //{
            //    Console.WriteLine($"{keyValuePair.Key} : {keyValuePair.Value}");
            //}

            //try
            //{
            //    computer.Close();
            //}
            //catch (Exception)
            //{
            //    //ignore closing errors
            //}

            var rand = new Random();

            for (uint i = 0; i < 4; i++)
            {
                cores.Add(new Core()
                {
                    Id = i,
                    Speed = (uint)rand.Next(900, 5400),
                    Temperature = (uint)rand.Next(75, 79)
                });
            }

            var tasks = new List<Task>();
            TcpListener server = new TcpListener(IPAddress.Parse("127.0.0.1"), 9090);
            server.Start();
            Console.WriteLine("Server has started on 127.0.0.1:9090.{0}Waiting for a connection...",
                Environment.NewLine);

            var cpuData = new CpuData()
            {
                Name = "unknown",
            };

            cpuData.Cores.AddRange(cores);

            var toClientData = new ToClientData()
            {
                Cpu = cpuData
            };

            while (true)
            {
                TcpClient client = await server.AcceptTcpClientAsync();
                Channel channel = new Channel(client, toClientData);
                var task = channel.Run();
                tasks.Add(task);
            }

            Console.WriteLine("Finished");
            Console.ReadLine();
        }
    }
}
