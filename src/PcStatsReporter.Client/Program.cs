using Grpc.Net.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace PcStatsReporter.Client
{
    public static class Program
    {
        public static async Task<int> Main()
        {
            // var scanner = new Scanner();
            //
            // var port = 11111;
            // var host = await scanner.Scan(port);
            //
            GrpcChannel channel = GrpcChannel.ForAddress("http://localhost:11111");
            
            var hostBuilder = CreateHostBuilder()
                .ConfigureServices(services =>
                {
                    services.AddHostedService<CpuCollector>();
                    services.AddHostedService<InitializeService>();
                    services.AddSingleton<IInitializer<ClientChannel>, GrpcInitializer>();
                    services.AddSingleton<IInitializer<Settings>, SettingsInitializer>();
                    services.AddSingleton<IInitializer<AppContext>, AppContextInitializer>();
                    services.AddSingleton<IInitializer<SettingsCollector>, SettingCollectorInitializer>();
                    services.AddSingleton<AppContext>();
                    services.AddSingleton<Settings>();
                    services.AddSingleton<ClientChannel>();
                    services.AddSingleton<SettingsCollector>();

                    services.AddSingleton(channel);
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