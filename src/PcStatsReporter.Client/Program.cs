﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PcStatsReporter.Client.CollectorServices;
using PcStatsReporter.Client.Initialization;
using PcStatsReporter.Client.Messages;
using PcStatsReporter.Client.NetworkScanner;
using PcStatsReporter.Core.Models;
using PcStatsReporter.Core.ServiceProviders;
using PcStatsReporter.GrpcClient.Maps;
using PcStatsReporter.LibreHardware;
using Rebus.Config;

namespace PcStatsReporter.Client
{
    public static class Program
    {
        public static async Task<int> Main()
        {
            var hostBuilder = CreateHostBuilder()
                .ConfigureServices(services =>
                {
                    services.AddHostedService<CpuCollectorService>();
                    services.AddHostedService<GpuCollectorService>();
                    services.AddHostedService<RamCollectorService>();
                    services.AddHostedService<InitService>();
                    services.AddTransient<IServiceFinder, HttpScanner>();
                    services.AddTransient<PcInfoCollector>();
                    services.AddSingleton<ICollector<CpuSample>, CpuCollector>();
                    services.AddSingleton<ICollector<GpuSample>, GpuCollector>();
                    services.AddSingleton<ICollector<RamSample>, RamCollector>();
                    services.AddSingleton<AppContext>();
                    services.AddSingleton<SettingsCollector>();
                    services.AddGrpcClientMaps();

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