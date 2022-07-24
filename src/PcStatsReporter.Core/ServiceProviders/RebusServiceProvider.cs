using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PcStatsReporter.Core.Messages;
using Rebus.Bus;
using Rebus.Config;
using Rebus.Persistence.InMem;
using Rebus.Routing.TypeBased;
using Rebus.Transport.InMem;

namespace PcStatsReporter.Core.ServiceProviders;

public static class RebusServiceProvider
{
    
    public static void AddReporterRebus(this IServiceCollection services)
    {
        var network = new InMemNetwork();
        var subscriberStore = new InMemorySubscriberStore();
        
        services.AddRebus(x =>
        {
            return x
                .Transport(t => t.UseInMemoryTransport(network, "queue"))
                .Routing(r => r.TypeBased().MapAssemblyOf<Registered>("PcStatsReporter"))
                .Subscriptions(s => s.StoreInMemory(subscriberStore));
        });
    }
    
    public static IBus UseReporterRebus(this IHost app)
    {
        var bus = app.Services.GetService<IBus>();
        return bus;
    }

}