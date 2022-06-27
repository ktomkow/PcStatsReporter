using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using OpenHardwareMonitor.Hardware;
using OpenHardwareMonitor.Collections;
using PcStatsReporter.Core.Models;

namespace PcStatsReporter.OpenHardware.Test.Unit
{
    [TestClass]
    public class SensorsExtensionsTests
    {
        [TestMethod]
        public void TryGetCoreId_WhenCpu1_Then1()
        {
            uint expected = 1;
            string input = "CPU Core #1";

            bool isSuccess = input.TryGetCoreId(out uint result);

            Assert.IsTrue(isSuccess);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TryGetCoreId_WhenCpu9_Then9()
        {
            uint expected = 9;
            string input = "CPU Core #9";

            bool isSuccess = input.TryGetCoreId(out uint result);

            Assert.IsTrue(isSuccess);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TryGetCoreId_WhenPackage_ThenFalseAnd0()
        {
            uint expected = default(uint);
            string input = "CPU Package";

            bool isSuccess = input.TryGetCoreId(out uint result);

            Assert.IsFalse(isSuccess);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
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

            Assert.AreEqual(core.Id, (uint) 1);
            Assert.AreEqual(core.Temperature, (uint) 56);
            Assert.AreEqual(core.Speed, (uint) 3456);
            Assert.AreEqual(core.Load, (uint) 63);
        }


        private class Sensor : ISensor
        {
            public SensorType SensorType { get; set; }

            public string Name { get; set; }

            public float? Value { get; set; }

            public int Index => throw new System.NotImplementedException();
            public Identifier Identifier => throw new System.NotImplementedException();
            public bool IsDefaultHidden => throw new System.NotImplementedException();
            public IReadOnlyArray<IParameter> Parameters => throw new System.NotImplementedException();
            public float? Min => throw new System.NotImplementedException();
            public float? Max => throw new System.NotImplementedException();
            public IEnumerable<SensorValue> Values => throw new System.NotImplementedException();
            public IControl Control => throw new System.NotImplementedException();
            public IHardware Hardware => throw new System.NotImplementedException();

            public void Accept(IVisitor visitor)
            {
                throw new System.NotImplementedException();
            }

            public void ResetMax()
            {
                throw new System.NotImplementedException();
            }

            public void ResetMin()
            {
                throw new System.NotImplementedException();
            }

            public void Traverse(IVisitor visitor)
            {
                throw new System.NotImplementedException();
            }
        }
    }
}