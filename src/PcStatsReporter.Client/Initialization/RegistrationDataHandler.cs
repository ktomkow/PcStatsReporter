using PcStatsReporter.Client.Messages;
using Rebus.Bus;
using Rebus.Handlers;

namespace PcStatsReporter.Client.Initialization;

public class RegistrationDataHandler : IHandleMessages<GetRegistrationDataCommand>
{
    private readonly IBus _bus;

    public RegistrationDataHandler(IBus bus)
    {
        _bus = bus;
    }
    
    public async Task Handle(GetRegistrationDataCommand message)
    {
        var @event = new GotRegistrationDataEvent()
        {
            CpuName = "My cpu",
            GpuName = "My GPU",
            TotalRam = 9.3
        };

        await _bus.Publish(@event);
    }
}