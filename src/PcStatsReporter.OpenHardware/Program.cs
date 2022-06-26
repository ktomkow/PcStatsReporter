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
            var computer = new Computer {CPUEnabled = true};
            computer.Open();

            var coreAndTemperature = new Dictionary<string, float>();

            foreach (var hardware in computer.Hardware)
            {
                hardware.Update(); //use hardware.Name to get CPU model
                uint i = 0;
                foreach (var sensor in hardware.Sensors)
                {
                    Console.WriteLine(sensor.SensorType + " " + sensor.Value.HasValue);
                    if (sensor.SensorType == SensorType.Temperature && sensor.Value.HasValue)
                    {
                        coreAndTemperature.Add(sensor.Name, sensor.Value.Value);

                        i++;
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

            Console.WriteLine("Finished");
            Console.ReadLine();
        }
    }
}