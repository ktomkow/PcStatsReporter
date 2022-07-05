using System;
using LibreHardwareMonitor.Hardware;

namespace PcStatsReporter.LibreHardware;

public static class Program
{
    public static void Main()
    {
        Console.WriteLine("Init");

        var computer = new Computer
        {
            IsCpuEnabled = true,
            IsGpuEnabled = true,
            IsPsuEnabled = true,
            IsMemoryEnabled = true,
            IsMotherboardEnabled = true,
            IsNetworkEnabled = true,
            IsControllerEnabled = true,
            IsBatteryEnabled = true,
            IsStorageEnabled = true
        };
        
        computer.Open();
        
        foreach (var hardware in computer.Hardware)
        {
            hardware.Update(); //use hardware.Name to get CPU model
            Console.WriteLine($"Hardware: {hardware.Name} : {hardware.Identifier}");
            foreach (var sensor in hardware.Sensors)
            {
                Console.WriteLine($"    {sensor.SensorType} - {sensor.Name} - {sensor.Value}");
            }
        }

        Console.WriteLine("Finished");
        Console.ReadLine();
    }
}