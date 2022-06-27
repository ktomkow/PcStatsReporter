using System.Collections.Generic;
using PcStatsReporter.Proto;

namespace PcStatsReporter.Server.Mappers
{
    internal static class CpuDataMap
    {
        internal static ToClient MapToClient(this Core.Models.CpuData cpuData)
        {
            var toClient = new ToClient()
            {
                Data = new ToClientData()
                {
                    Cpu = new CpuData()
                    {
                        Name = cpuData.Name
                    }
                }
            };

            var cores = MapCores(cpuData);

            foreach (var core in cores)
            {
                toClient.Data.Cpu.Cores.Add(core);
            }

            return toClient;
        }

        private static List<Proto.Core> MapCores(Core.Models.CpuData cpuData)
        {
            var result = new List<Proto.Core>();

            foreach (var core in cpuData.Cores)
            {
                var transportCore = new Proto.Core()
                {
                    Id = core.Id,
                    Load = core.Load,
                    Speed = core.Speed,
                    Temperature = core.Temperature
                };
                
                result.Add(transportCore);
            }

            return result;
        }
    }
}