using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace PcStatsReporter.Client
{
    public static class Program
    {
        public static async Task<int> Main()
        {
            var host = CreateHostBuilder()
                .ConfigureServices(services =>
                {
                    services.AddHostedService<SampleHostedService>();
                });
            
            await host.RunConsoleAsync();
            return Environment.ExitCode;
        }

        private static IHostBuilder CreateHostBuilder()
        {
            return Host.CreateDefaultBuilder();
        }
    }
}