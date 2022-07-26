using PcStatsReporter.Core.Maps;
using PcStatsReporter.Core.Models;
using PcStatsReporter.Grpc.Proto;

namespace PcStatsReporter.GrpcClient.Maps;

public class CpuSampleMap : IMap<CpuSample, CollectedData>
{
    public CollectedData Map(CpuSample source)
    {
        CollectedData result = new();
        result.Uuid.Value = source.Id.ToString();

        CollectedCpuData cpuData = new CollectedCpuData
        {
            Temperature = source.Temperature,
            AverageLoad = source.AverageLoad
        };

        foreach (var sourceCore in source.Cores)
        {
            var core = new CollectedCpuCoreData()
            {
                Id = sourceCore.CoreNumber,
                Speed = sourceCore.Speed,
                Temperature = sourceCore.Temperature
            };

            foreach ((uint threadNumber, uint threadLoad) sourceThread in sourceCore.ThreadsLoad)
            {
                var thread = new CollectedCpuThreadData()
                {
                    Id = sourceThread.threadNumber,
                    Load = sourceThread.threadLoad
                };

                core.Threads.Add(thread);
            }

            cpuData.Cores.Add(core);
        }

        result.Cpu = cpuData;

        return result;
    }
}