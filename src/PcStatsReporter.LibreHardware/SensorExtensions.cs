using System;
using System.Collections.Generic;
using System.Linq;
using LibreHardwareMonitor.Hardware;
using PcStatsReporter.Core.Models;

namespace PcStatsReporter.LibreHardware
{
    public static class SensorsExtensions
    {
        public static bool TryGetCoreId(this string name, out uint result)
        {
            string cutNumber = name.ToLowerInvariant().Replace("cpu core #", "").Split(' ').First();

            return uint.TryParse(cutNumber, out result);
        }

        public static (double used, double available) GetRam(this IEnumerable<ISensor> sensors)
        {
            var ramSensors = sensors
                .Where(x => x.SensorType == SensorType.Data)
                .Where(x => x.Value.HasValue)
                .ToList();

                var usedSensor = ramSensors.FirstOrDefault(x =>
                    x.Name.Contains("memory used", StringComparison.InvariantCultureIgnoreCase));

                var used = usedSensor.Value;
                
                var availableSensor = ramSensors.FirstOrDefault(x =>
                    x.Name.Contains("memory available", StringComparison.InvariantCultureIgnoreCase));

                var available = availableSensor.Value;

                return ((double) used, (double) available);
        }

        public static uint GetPackageTemperature(this IEnumerable<ISensor> sensors)
        {
            ISensor? sensor = sensors
                .Where(x => x.SensorType == SensorType.Temperature)
                .Where(x => x.Value.HasValue)
                .FirstOrDefault(x => x.Name.Contains("cpu package", StringComparison.InvariantCultureIgnoreCase));

            if (sensor is null)
            {
                return default;
            }

            return (uint)sensor.Value;
        }
        
        public static uint GetAverageLoad(this IEnumerable<ISensor> sensors)
        {
            ISensor? sensor = sensors
                .Where(x => x.SensorType == SensorType.Load)
                .Where(x => x.Value.HasValue)
                .FirstOrDefault(x => x.Name.Contains("cpu total", StringComparison.InvariantCultureIgnoreCase));

            if (sensor is null)
            {
                return default;
            }

            return (uint)sensor.Value;
        }

        public static List<CpuCore> GetCpuCores(this IEnumerable<ISensor> sensors)
        {
            Dictionary<uint, CpuCore> cores = new Dictionary<uint, CpuCore>();

            foreach (var sensor in sensors)
            {
                if (sensor.Value.HasValue == false)
                {
                    continue;
                }

                bool isCoreInfo = sensor.Name.TryGetCoreId(out uint coreId);
                if (isCoreInfo == false)
                {
                    continue;
                }

                if (cores.ContainsKey(coreId) == false)
                {
                    CpuCore newCore = new CpuCore() {Id = coreId};
                    cores.Add(coreId, newCore);
                }

                CpuCore core = cores[coreId];

                switch (sensor.SensorType)
                {
                    case SensorType.Temperature:
                        core.Temperature = (uint) sensor.Value;
                        break;

                    case SensorType.Clock:
                        core.Speed = (uint) sensor.Value;
                        break;

                    case SensorType.Load:
                        core.Load.Add((uint) sensor.Value);
                        break;
                }
            }

            return cores.Values.ToList();
        }
    }
}