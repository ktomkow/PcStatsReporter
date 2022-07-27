using System.Threading.Tasks;

namespace PcStatsReporter.AspNetCore.SignalR;

public interface IReporterHub
{
    Task SendCpuData();
}