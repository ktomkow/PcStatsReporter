using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using PcStatsReporter.Grpc.Services;

namespace PcStatsReporter.AspNetCore.Grpc;

public static class ServiceProvider
{
    public static void AddReporterGrpc(this IServiceCollection services)
    {
        services.AddGrpc();
    }
    
    public static void UseReporterGrpc(this WebApplication app)
    {
        app.MapGrpcService<CalculatorService>();
        // app.MapGrpcService<CollectorService>();
        app.MapGrpcService<GrpcCollectorService>();
        app.MapGrpcService<RegistrationService>();
        app.MapGrpcService<SettingsService>();
    }
}