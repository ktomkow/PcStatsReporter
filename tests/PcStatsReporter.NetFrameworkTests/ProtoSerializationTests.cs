using Google.Protobuf;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PcStatsReporter.Proto;
using System;

namespace PcStatsReporter.NetFrameworkTests
{
    [TestClass]
    public class ProtoSerializationTests
    {
        [TestMethod]
        public void SerializeAndDeserialize_CoreMessage()
        {
            Core core = new Core()
            {
                Id = 1,
                Speed = 1251,
                Temperature = 40
            };

            byte[] serialized = core.ToByteArray();
            Core deserialized = Core.Parser.ParseFrom(serialized);

            Assert.AreEqual(core.Id, deserialized.Id);
            Assert.AreEqual(core.Speed, deserialized.Speed);
            Assert.AreEqual(core.Temperature, deserialized.Temperature);
        }

        [TestMethod]
        public void SerializeAndDeserialize_SendDataMessage()
        {
            var toServerSendData = new ToServer
            {
                Command = new ToServerCommand()
                {
                    SendData = new SendData()
                }
            };

            byte[] serialized = toServerSendData.ToByteArray();
            ToServer deserialized = ToServer.Parser.ParseFrom(serialized);

            Assert.IsNotNull(deserialized.Command);
            Assert.IsNotNull(deserialized.Command.SendData);
        }
    }
}
