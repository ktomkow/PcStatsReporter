using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using PcStatsReporter.AspNetCore;
using PcStatsReporter.AspNetCore.ServiceProviders;
using PcStatsReporter.Core.Persistence;
using PcStatsReporter.Core.ReportingClientSettings;
using PcStatsReporter.Grpc;
using PcStatsReporter.LibreHardware;

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

DefaultSetting defaultSetting = new DefaultSetting()
{
    Period = TimeSpan.FromSeconds(1)
};

builder.Services.AddSingleton(defaultSetting);
var app = builder.Build();

app.UseCustomSwagger();

app.UseRouting();
app.MapControllers();

app.UseCors(x => x
    .AllowAnyHeader()
    .AllowAnyMethod()
    .AllowAnyOrigin());


app.UseReporterGrpc();

app.Run();