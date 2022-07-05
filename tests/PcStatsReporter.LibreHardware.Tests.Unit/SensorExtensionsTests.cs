using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using LibreHardwareMonitor.Hardware;
using PcStatsReporter.Core.Models;
using Xunit;

namespace PcStatsReporter.LibreHardware.Tests.Unit
{
    public class SensorsExtensionsTests
    {
        [Fact]
        public void TryGetCoreId_WhenCpu1_Then1()
        {
            uint expected = 1;
            string input = "CPU Core #1";

            bool isSuccess = input.TryGetCoreId(out uint result);

            isSuccess.Should().BeTrue();
            result.Should().Be(expected);
        }

        [Fact]
        public void TryGetCoreId_WhenCpu9_Then9()
        {
            uint expected = 9;
            string input = "CPU Core #9";

            bool isSuccess = input.TryGetCoreId(out uint result);

            isSuccess.Should().BeTrue();
            result.Should().Be(expected);
        }

        [Fact]
        public void TryGetCoreId_WhenPackage_ThenFalseAnd0()
        {
            uint expected = default(uint);
            string input = "CPU Package";

            bool isSuccess = input.TryGetCoreId(out uint result);

            isSuccess.Should().BeFalse();
            result.Should().Be(expected);
        }

        [Fact]
        public void GetCores()
        {
            List<ISensor> sensors = new List<ISensor>()
            {
                new Sensor()
                {
                    Name = "CPU Core #1",
                    SensorType = SensorType.Temperature,
                    Value = 56.3f
                },
                new Sensor()
                {
                    Name = "CPU Core #1",
                    SensorType = SensorType.Clock,
                    Value = 3456.1f
                },
                new Sensor()
                {
                    Name = "CPU Core #1",
                    SensorType = SensorType.Load,
                    Value = 63.49f
                },
            };

            List<CpuCore> cores = sensors.GetCpuCores();
            CpuCore core = cores.Single();

            core.Id.Should().Be((uint) 1);
            core.Temperature.Should().Be((uint) 56);
            core.Speed.Should().Be((uint) 3456);
            core.Load.Should().Be((uint) 63);
        }

        [Fact]
        public void GetPackageTemperature_When60_Then60()
        {
            List<ISensor> sensors = new List<ISensor>()
            {
                new Sensor()
                {
                    Name = "CPU Package",
                    SensorType = SensorType.Temperature,
                    Value = 60.0f
                },
            };

            uint result = sensors.GetPackageTemperature();

            result.Should().Be((uint)60);
        }
        
        [Fact]
        public void GetAverageLoad_When33_Then33()
        {
            List<ISensor> sensors = new List<ISensor>()
            {
                new Sensor()
                {
                    Name = "CPU Total",
                    SensorType = SensorType.Load,
                    Value = 33.0f
                },
            };

            uint result = sensors.GetAverageLoad();

            result.Should().Be((uint)33);
        }
        
        private class Sensor : ISensor
        {
            public IReadOnlyList<IParameter> Parameters { get; }
            public SensorType SensorType { get; set; }

            public void ResetMin()
            {
                throw new NotImplementedException();
            }

            public void ResetMax()
            {
                throw new NotImplementedException();
            }

            public IControl Control { get; }
            public IHardware Hardware { get; }
            public Identifier Identifier { get; }
            public int Index { get; }
            public bool IsDefaultHidden { get; }
            public float? Max { get; }
            public float? Min { get; }
            public string Name { get; set; }

            public float? Value { get; set; }
            public IEnumerable<SensorValue> Values { get; }
            public TimeSpan ValuesTimeWindow { get; set; }

            public void Accept(IVisitor visitor)
            {
                throw new NotImplementedException();
            }

            public void Traverse(IVisitor visitor)
            {
                throw new NotImplementedException();
            }
        }
    }
}