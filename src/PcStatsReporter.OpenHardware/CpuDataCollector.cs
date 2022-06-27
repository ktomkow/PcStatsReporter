using OpenHardwareMonitor.Hardware;
using PcStatsReporter.Core;
using System;
using System.Threading.Tasks;
using PcStatsReporter.Core.Models;

namespace PcStatsReporter.OpenHardware
{
    public class CpuDataCollector : IDisposable
    {
        private readonly Store store;
        private readonly Computer computer;
        private readonly TimeSpan period = TimeSpan.FromMilliseconds(500);

        public CpuDataCollector(Store store)
        {
            this.computer = new Computer
            {
                CPUEnabled = true
            };

            this.computer.Open();

            this.store = store;
        }

        public async Task Start()
        {
            while (true)
            {
                foreach (var hardware in computer.Hardware)
                {
                    hardware.Update(); //use hardware.Name to get CPU model
                    CpuData cpu = new CpuData
                    {
                        Name = hardware.Name,
                        Cores = hardware.Sensors.GetCpuCores()
                    };

                    store.Set(cpu);
                }
                
                await Task.Delay(this.period);
            }
        }

        public void Dispose()
        {
            computer.Close();
        }
    }
}