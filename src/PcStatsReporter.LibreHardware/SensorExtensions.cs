﻿using System.Collections.Generic;
using System.Linq;
using LibreHardwareMonitor.Hardware;
using PcStatsReporter.Core.Models;

namespace PcStatsReporter.LibreHardware
{
    public static class SensorsExtensions
    {
        public static bool TryGetCoreId(this string name, out uint result)
        {
            string cutNumber = name.ToLowerInvariant().Replace("cpu core #", "");

            return uint.TryParse(cutNumber, out result);
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
                        core.Load = (uint) sensor.Value;
                        break;
                }
            }

            return cores.Values.ToList();
        }
    }
}