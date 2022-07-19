using System.Threading.Tasks;
using Grpc.Core;
using PcStatsReporter.Grpc.Proto;

namespace PcStatsReporter.Grpc.Services;

public class RegistrationService : Registerer.RegistererBase
{
    public override Task<RegistrationResponse> Register(Registration request, ServerCallContext context)
    {
        return base.Register(request, context);
    }
}