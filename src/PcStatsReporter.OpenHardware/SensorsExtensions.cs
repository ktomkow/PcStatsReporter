using OpenHardwareMonitor.Hardware;
using PcStatsReporter.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PcStatsReporter.OpenHardware
{
    public static class SensorsExtensions
    {
        public static bool TryGetCoreId(this string name, out uint result)
        {
            string cutNumber = name.ToLowerInvariant().Replace("cpu core #", "");
            
            return uint.TryParse(cutNumber, out result);
        }
        
        public static List<CpuCore> GetCores(this IEnumerable<ISensor> sensors)
        {
            Dictionary<uint, CpuCore> cores = new Dictionary<uint, CpuCore>();

            foreach (var sensor in sensors)
            {
                if(sensor.Value.HasValue == false)
                {
                    continue;
                }

                // Console.WriteLine(sensor.SensorType + " " + sensor.Value.HasValue);
                // if (sensor.SensorType == SensorType.Temperature && sensor.Value.HasValue)
                // {
                //     // coreAndTemperature.Add(sensor.Name, sensor.Value.Value);
                //     Console.WriteLine(
                //         $"FIRST LOOP sensor.Name {sensor.Name}, sensor.Value.Value {sensor.Value}");
                // }

                if (sensor.Value.HasValue)
                {
                    Console.WriteLine(
                        $"sensor.Name {sensor.Name}, sensor.Value {sensor.Value}, sensor.SensorType {sensor.SensorType}");
                }

                if (sensor.Value.HasValue)
                {
                    switch (sensor.SensorType)
                    {
                        case SensorType.Temperature:
                            break;

                        case SensorType.Clock:
                            break;

                        case SensorType.Load:
                            break;
                    }
                }
            }

            return cores.Values.ToList();
        }
    }
}
