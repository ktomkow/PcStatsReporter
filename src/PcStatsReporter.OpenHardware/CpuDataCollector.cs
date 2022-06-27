using OpenHardwareMonitor.Hardware;
using PcStatsReporter.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PcStatsReporter.Core.Models;

namespace PcStatsReporter.OpenHardware
{
    public class CpuDataCollector : IDisposable
    {
        private readonly Store store;
        private readonly Computer computer;
        private readonly TimeSpan period = TimeSpan.FromSeconds(1000);

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
                    CpuData cpu = new CpuData();
                    Dictionary<uint, CpuCore> cores = new Dictionary<uint, CpuCore>();

                    foreach (var sensor in hardware.Sensors)
                    {
 
                        // Console.WriteLine(sensor.SensorType + " " + sensor.Value.HasValue);
                        // if (sensor.SensorType == SensorType.Temperature && sensor.Value.HasValue)
                        // {
                        //     // coreAndTemperature.Add(sensor.Name, sensor.Value.Value);
                        //     Console.WriteLine(
                        //         $"FIRST LOOP sensor.Name {sensor.Name}, sensor.Value.Value {sensor.Value}");
                        // }

                        if (sensor.Value.HasValue)
                        {
                            Console.WriteLine(
                                $"sensor.Name {sensor.Name}, sensor.Value {sensor.Value}, sensor.SensorType {sensor.SensorType}");
                        }

                        if (sensor.Value.HasValue)
                        {
                            switch (sensor.SensorType)
                            {
                                case SensorType.Temperature:
                                    break;
                                
                                case SensorType.Clock:
                                    break;
                                
                                case SensorType.Load:
                                    break;
                            }
                        }
                    }

                    cpu.Cores = cores.Values;
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