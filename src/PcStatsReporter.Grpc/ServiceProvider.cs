using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using PcStatsReporter.Grpc.Services;

namespace PcStatsReporter.Grpc;

public static class ServiceProvider
{
    public static void AddReporterGrpcServer(this IServiceCollection services)
    {
        services.AddGrpc();
    }
    
    public static void UseReporterGrpcServer(this WebApplication app)
    {
        app.MapGrpcService<CollectorService>();
        app.MapGrpcService<RegistrationService>();
        app.MapGrpcService<SettingsService>();
    }
}