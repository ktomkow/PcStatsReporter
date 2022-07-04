using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using PcStatsReporter.AspNetCore.Mappers;
using PcStatsReporter.AspNetCore.Mappers.Maps;
using PcStatsReporter.Core.Models;
using PcStatsReporter.Grpc;
using PcStatsReporter.LibreHardware;
using PcStatsReporter.RestContracts;

var builder = WebApplication.CreateBuilder(args);

// for grpc
builder.WebHost.ConfigureKestrel(options =>
{
    // Setup a HTTP/2 endpoint without TLS.
    options.ListenLocalhost(5287, o => o.Protocols =
        HttpProtocols.Http2);
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo(){Title = "PC Hardware Monitor", Version = "v1"});
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});


builder.Services.AddSingleton<IMap<CpuData, CpuResponse>, CpuMap>();
builder.Services.AddSingleton<CpuDataCollector>();

builder.Services.UseReporterGrpc();

var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    c.RoutePrefix = "swagger";
});


app.UseRouting();
app.MapControllers();

app.UseCors(x => x
    .AllowAnyHeader()
    .AllowAnyMethod()
    .AllowAnyOrigin());


app.AddReporterGrpc();

app.Run();