using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using PcStatsReporter.Core.Messages;
using PcStatsReporter.Core.Persistence;
using Rebus.Handlers;

namespace PcStatsReporter.AspNetCore.Handlers;

public class ReportingClientRegisteredEventHandler : IHandleMessages<ReportingClientRegisteredEvent>
{
    private readonly ILogger<ReportingClientRegisteredEventHandler> _logger;
    private readonly IHold _holder;

    public ReportingClientRegisteredEventHandler(ILogger<ReportingClientRegisteredEventHandler> logger ,IHold holder)
    {
        _logger = logger;
        _holder = holder;
    }

    public async Task Handle(ReportingClientRegisteredEvent message)
    {
        await _holder.Set(message.PcInfo);
    }
}