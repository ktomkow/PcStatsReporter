using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using PcStatsReporter.Grpc.Services;

namespace PcStatsReporter.Grpc;

public static class ServiceProvider
{
    public static void UseReporterGrpc(this IServiceCollection services)
    {
        services.AddGrpc();
    }

    public static void AddReporterGrpc(this WebApplication app)
    {
        app.MapGrpcService<CalculatorService>();

        // app.UseEndpoints(endpoints =>
        // {
        //     endpoints.MapGrpcService<CalculatorService>();
        // });
    }
}