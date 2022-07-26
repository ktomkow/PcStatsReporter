using System.Threading.Tasks;
using Grpc.Core;
using PcStatsReporter.Grpc.Proto;

namespace PcStatsReporter.Grpc.Services;

public class CollectorService : Collector.CollectorBase
{
    public override Task<DataResponse> Collect(CollectedData request, ServerCallContext context)
    {
        return base.Collect(request, context);
    }
}