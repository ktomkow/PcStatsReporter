using System;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using PcStatsReporter.AspNetCore.Grpc;
using PcStatsReporter.AspNetCore.Handlers;
using PcStatsReporter.AspNetCore.Mappers;
using PcStatsReporter.AspNetCore.Messages;
using PcStatsReporter.AspNetCore.ServiceProviders;
using PcStatsReporter.AspNetCore.SignalR;
using PcStatsReporter.Core.Messages;
using PcStatsReporter.Core.Persistence;
using PcStatsReporter.Core.ReportingClientSettings;
using PcStatsReporter.Core.ServiceProviders;
using PcStatsReporter.Grpc;
using PcStatsReporter.LibreHardware;
using Rebus.Bus;
using Rebus.Config;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureKestrel();

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddCustomSwagger();

builder.Services.AddMaps();

builder.Services.AddSingleton<CpuDataCollector>();
builder.Services.AddSingleton<RamDataCollector>();
builder.Services.AddSingleton<GpuDataCollector>();

builder.Services.AddSingleton<IHold, MemoryHold>();

builder.Services.AddReporterGrpc();

builder.Services.AddServerMaps();

DefaultSetting defaultSetting = new DefaultSetting()
{
    Period = TimeSpan.FromSeconds(1)
};

builder.Services.AddSingleton(defaultSetting);

builder.Services.AddReporterRebus();
builder.Services.AutoRegisterHandlersFromAssemblyOf<RegisteredHandler>();

builder.Services.AddSignalR();

var app = builder.Build();

app.UseCustomSwagger();

app.UseRouting();
app.MapControllers();


app.UseCors(corsBuilder =>
{
    corsBuilder.WithOrigins("http://localhost:8080")
        .AllowAnyHeader()
        .WithMethods("GET", "POST")
        .AllowCredentials();
});


var bus = app.UseReporterRebus();
await bus.Subscribe<Registered>();
await bus.Subscribe<CpuSampleArrived>();

app.UseReporterGrpc();

app.MapHub<ReporterHub>("/reporter");

app.Run();