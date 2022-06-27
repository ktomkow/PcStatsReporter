using Google.Protobuf;
using PcStatsReporter.Proto;
using System;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading.Tasks;
using PcStatsReporter.Core;
using PcStatsReporter.Server.Mappers;
using CpuData = PcStatsReporter.Core.Models.CpuData;

namespace PcStatsReporter.Server
{
    public class Channel
    {
        public Guid Id { get; }
        private string ShortId => Id.ToString().Substring(0, 4);
        private readonly TcpClient _tcpClient;
        private readonly Store store;
        public ChannelState State { get; private set; }

        public Channel(TcpClient tcpClient, Store store)
        {
            _tcpClient = tcpClient;
            this.store = store;
            State = ChannelState.Created;
            Id = Guid.NewGuid();
        }

        public async Task Run()
        {
            if (State != ChannelState.Created)
            {
                await Task.CompletedTask;
                return;
            }

            Console.WriteLine($"{ShortId} Started");

            State = ChannelState.Started;

            NetworkStream stream = _tcpClient.GetStream();

            while (_tcpClient.GetState() == TcpState.Established)
            {
                //Console.WriteLine("While");
                while (_tcpClient.Available < 2 && _tcpClient.GetState() == TcpState.Established)
                {
                    //Console.WriteLine("Waiting for data");
                    await Task.Delay(TimeSpan.FromMilliseconds(10));
                }

                while (stream.DataAvailable)
                {
                    //Console.WriteLine("Inside while stream.DataAvailable");
                    byte[] buffer = new byte[_tcpClient.Available];

                    //Console.WriteLine($"_tcpClient.Available : {_tcpClient.Available}");
                    await stream.ReadAsync(buffer, 0, buffer.Length);

                    //Console.WriteLine($"Read data : {buffer}, length: {buffer.Length}");

                    //foreach (var b in buffer)
                    //{
                    //    Console.WriteLine(b);
                    //}

                    try
                    {
                        var toServer2 = ToServer.Parser.ParseFrom(buffer);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Exception : {e.GetType().Name} {e.Message}");
                    }

                    var toServer = ToServer.Parser.ParseFrom(buffer);

                    //Console.WriteLine($"ToServer null? >> {toServer is null} <<");

                    var command = toServer.Command;

                    //Console.WriteLine($"Command null? >> {command is null} <<");

                    switch (command.CommandCase)
                    {
                        case ToServerCommand.CommandOneofCase.Disconnect:
                            Console.WriteLine("Disconnecting.. but I do not know how to o_0");
                            // todo
                            break;

                        case ToServerCommand.CommandOneofCase.SendData:
                            //Console.WriteLine("Send Data Request");
                            var cpu = this.store.Get<CpuData>();
                            var toClient = cpu.MapToClient();

                            var toClientBytes = toClient.ToByteArray();

                            await stream.WriteAsync(toClientBytes, 0, toClientBytes.Length);
                            await stream.FlushAsync();
                            break;

                        default:
                            break;
                    }
                }
            }

            Console.WriteLine($"{ShortId} Finished");
            State = ChannelState.Finished;
            await Task.CompletedTask;
        }
    }
}