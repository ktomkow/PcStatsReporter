using System.Threading.Tasks;
using Grpc.Core;
using PcStatsReporter.Grpc.Proto;

namespace PcStatsReporter.Grpc.Services;

public class SettingsService : SettingsManager.SettingsManagerBase
{
    public override Task<SettingsResponse> Get(SettingsRequest request, ServerCallContext context)
    {
        return base.Get(request, context);
    }
}