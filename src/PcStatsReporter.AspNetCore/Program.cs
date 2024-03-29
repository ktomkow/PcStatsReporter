using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PcStatsReporter.AspNetCore.Configuration;
using PcStatsReporter.AspNetCore.DummyClient;
using PcStatsReporter.AspNetCore.Handlers;
using PcStatsReporter.AspNetCore.Mappers;
using PcStatsReporter.AspNetCore.ServiceProviders;
using PcStatsReporter.AspNetCore.SignalR;
using PcStatsReporter.Core.Messages;
using PcStatsReporter.Core.Persistence;
using PcStatsReporter.Core.ReportingClientSettings;
using PcStatsReporter.Core.ServiceProviders;
using PcStatsReporter.Grpc;
using Rebus.Config;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureKestrel();

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddCustomSwagger();

builder.Services.AddMaps();

builder.Services.AddSingleton<IHold, MemoryHold>();

builder.Services.AddReporterGrpcServer();

builder.Services.AddServerMaps();

DefaultSetting defaultSetting = new DefaultSetting()
{
    Period = TimeSpan.FromSeconds(1)
};

builder.Services.AddSingleton(defaultSetting);

builder.Services.AddReporterRebus();
builder.Services.AutoRegisterHandlersFromAssemblyOf<RegisteredHandler>();

builder.Services.AddSignalR();

BuiltInCollectorSettings builtInCollectorSettings = new();
builder.Configuration.GetSection(nameof(BuiltInCollectorSettings).Replace("Settings",string.Empty)).Bind(builtInCollectorSettings);

if (builtInCollectorSettings.UseDummyClient)
{
    builder.Services.AddDummyClient();
}

builder.Services.AddSingleton<IConfigPrinter, ConfigPrinter>();

var app = builder.Build();

app.UseCustomSwagger();

app.UseRouting();
app.MapControllers();

app.UseCors(corsBuilder =>
{
    corsBuilder
        .SetIsOriginAllowed(_ => true)
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials();
});


var bus = app.UseReporterRebus();
await bus.Subscribe<ReportingClientRegisteredEvent>();
await bus.Subscribe<CpuSampleArrivedEvent>();
await bus.Subscribe<GpuSampleArrivedEvent>();
await bus.Subscribe<RamSampleArrivedEvent>();

app.UseReporterGrpcServer();

app.MapHub<ReporterHub>("/reporter");

var logger = app.Services.GetService<ILogger<Program>>();
if (logger is null)
{
    throw new ApplicationException("LOGGER IS NOT CONFIGURED!");
}

var configPrinter = app.Services.GetService<IConfigPrinter>();
if (configPrinter is null)
{
    logger.LogCritical("CONFIG PRINTER IS NULL!");
}
else
{ 
    configPrinter.Print();
}

app.Run();