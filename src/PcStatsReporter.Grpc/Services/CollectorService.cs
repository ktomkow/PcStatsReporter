using System.Threading.Tasks;
using Grpc.Core;
using PcStatsReporter.Grpc.Proto;

namespace PcStatsReporter.Grpc.Services;

public abstract class CollectorService : Collector.CollectorBase
{

}