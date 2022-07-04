using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace PcStatsReporter.AspNetCore.ServiceProviders;

public static class KestrelConfigurationProvider
{
    public static void ConfigureKestrel(this WebApplicationBuilder builder)
    {
        builder.WebHost.ConfigureKestrel(options =>
        {
            var portsAsStrings = builder.Configuration.GetSection("Ports").Value;
            var ports = portsAsStrings.Split(";").Select(int.Parse).ToList();
    
            // Setup a HTTP/2 endpoint without TLS for grpc
            options.ListenLocalhost(ports.First(), o => o.Protocols =
                HttpProtocols.Http2);
    
            // Setup a HTTP/1 endpoint without TLS for web clients
            options.ListenLocalhost(ports.Last(), o => o.Protocols =
                HttpProtocols.Http1);
        });
    }
}