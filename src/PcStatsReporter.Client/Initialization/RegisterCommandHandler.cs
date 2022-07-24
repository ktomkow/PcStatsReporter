using PcStatsReporter.Client.Messages;
using Rebus.Bus;
using Rebus.Handlers;

namespace PcStatsReporter.Client.Initialization;

public class RegisterCommandHandler : IHandleMessages<RegisterCommand>
{
    private readonly AppContext _appContext;
    private readonly IBus _bus;

    public RegisterCommandHandler(AppContext appContext, IBus bus)
    {
        _appContext = appContext;
        _bus = bus;
    }
    
    public async Task Handle(RegisterCommand message)
    {
        var @event = new RegistrationCompletedEvent();

        await _bus.Publish(@event);
    }
}