using System.Threading.Tasks;
using PcStatsReporter.AspNetCore.Messages;
using Rebus.Handlers;

namespace PcStatsReporter.AspNetCore.Handlers;

public class CpuSampleArrivedHandler : IHandleMessages<CpuSampleArrived>
{
    public async Task Handle(CpuSampleArrived message)
    {
        await Task.CompletedTask;
    }
}