using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.DependencyInjection;
using PcStatsReporter.AspNetCore.Mappers;
using PcStatsReporter.AspNetCore.Mappers.Maps;
using PcStatsReporter.AspNetCore.ServiceProviders;
using PcStatsReporter.Core.Models;
using PcStatsReporter.Grpc;
using PcStatsReporter.LibreHardware;
using PcStatsReporter.RestContracts;

var builder = WebApplication.CreateBuilder(args);

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

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddCustomSwagger();

builder.Services.AddSingleton<IMap<CpuData, CpuResponse>, CpuMap>();
builder.Services.AddSingleton<CpuDataCollector>();

builder.Services.UseReporterGrpc();

var app = builder.Build();


app.UseCustomSwagger();


app.UseRouting();
app.MapControllers();

app.UseCors(x => x
    .AllowAnyHeader()
    .AllowAnyMethod()
    .AllowAnyOrigin());


app.AddReporterGrpc();

app.Run();