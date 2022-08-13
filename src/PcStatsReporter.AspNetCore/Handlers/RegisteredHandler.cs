using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using PcStatsReporter.Core.Messages;
using Rebus.Handlers;

namespace PcStatsReporter.AspNetCore.Handlers;

public class RegisteredHandler : IHandleMessages<ReportingClientRegisteredEvent>
{
    private readonly ILogger<RegisteredHandler> _logger;

    public RegisteredHandler(ILogger<RegisteredHandler> logger)
    {
        _logger = logger;
    }
    
    public async Task Handle(ReportingClientRegisteredEvent message)
    {
        _logger.LogInformation($"Registered arrived {message.Id}");
        await Task.CompletedTask;
    }
}