using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PcStatsReporter.Client.NetworkScanner;

namespace PcStatsReporter.Client
{
    public static class Program
    {
        public static async Task<int> Main()
        {
            var scanner = new Scanner();

            var port = 11111;
            var host = await scanner.Scan(port);
            
            var hostBuilder = CreateHostBuilder()
                .ConfigureServices(services =>
                {
                    services.AddHostedService<SampleHostedService>();
                });

            await hostBuilder.RunConsoleAsync();
            return Environment.ExitCode;
        }

        private static IHostBuilder CreateHostBuilder()
        {
            return Host.CreateDefaultBuilder();
        }
    }
}