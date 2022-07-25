using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PcStatsReporter.Client.Initialization;
using PcStatsReporter.Client.Messages;
using PcStatsReporter.Client.NetworkScanner;
using PcStatsReporter.Core.ServiceProviders;
using PcStatsReporter.LibreHardware;
using Rebus.Config;

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
            // GrpcChannel channel = GrpcChannel.ForAddress("http://localhost:11111");
            
            var hostBuilder = CreateHostBuilder()
                .ConfigureServices(services =>
                {
                    services.AddHostedService<CpuCollector>();
                    services.AddHostedService<InitService>();
                    services.AddHostedService<SampleHostedService>();
                    services.AddTransient<Scanner>();
                    services.AddTransient<PcInfoCollector>();

                    services.AddSingleton<AppContext>();
                    services.AddSingleton<SettingsCollector>();
                    services.AddReporterRebus();  
                    services.AutoRegisterHandlersFromAssemblyOf<InitializationSaga>();
                    services.AddHttpClient();
                });


            IHost? app = hostBuilder.Build();

            var bus = app.UseReporterRebus();
            await bus.Subscribe<InitializeCommand>();
            await bus.Subscribe<GotRegistrationDataEvent>();
            await bus.Subscribe<RegistrationCompletedEvent>();
            await bus.Subscribe<GrpcInitializedEvent>();
            
            await bus.Subscribe<GrpcInitializeCommand>();
            await bus.Subscribe<GetRegistrationDataCommand>();
            await bus.Subscribe<RegisterCommand>();
            
            await bus.Subscribe<SettingsChanged>();

            await app.RunAsync();

            // await hostBuilder.RunConsoleAsync();
            
            return Environment.ExitCode;
        }

        private static IHostBuilder CreateHostBuilder()
        {
            return Host.CreateDefaultBuilder();
        }
    }
}