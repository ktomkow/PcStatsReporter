using System.Diagnostics;
using System.Net.Sockets;
using System.Text;
using Google.Protobuf;
using PcStatsReporter.Proto;

namespace PcStatsReporter.Client
{
    public static class Program
    {
        public static async Task Main()
        {
            Console.WriteLine("Client Init");

            TcpClient tcpClient = new TcpClient("127.0.0.1", 9090);

            Console.WriteLine($"tcpClient.Connected {tcpClient.Connected}");

            Stopwatch stopwatch = new Stopwatch();
            int counter = 0;
            int maxCount = 100;

            stopwatch.Start();


            var toServerSendData = new ToServer
            {
                Command = new ToServerCommand()
                {
                    SendData = new SendData()
                }
            };

            var stream = tcpClient.GetStream();

            Console.WriteLine("Let's go");
            int i = 0;
            
            while (counter <= maxCount)
            {
                //Console.WriteLine($"Loop no. {i}");
                // var line = Console.ReadLine();
                //
                // byte[] payload = Encoding.UTF8.GetBytes(line);
                //
                //
                // await tcpClient.GetStream().WriteAsync(payload, 0, payload.Length);

                byte[] toServerPayload = toServerSendData.ToByteArray();

                
                //Console.WriteLine("Sending data request");
                
                //foreach (var b in toServerPayload)
                //{
                //    Console.WriteLine(b);
                //}
                
                await stream.WriteAsync(toServerPayload, 0, toServerPayload.Length);
                await stream.FlushAsync();
                
                //Console.WriteLine($"Sending data request done. Length: {toServerPayload.Length}");

                await Task.Delay(TimeSpan.FromMilliseconds(50));

                await WaitForData(tcpClient);

                byte[] toClientPayload = new byte[tcpClient.Available];
                await stream.ReadAsync(toClientPayload);

                var toClient = ToClient.Parser.ParseFrom(toClientPayload);
                
                var cpu = toClient.Data.Cpu;
                //Console.WriteLine("*******************************");
                //Console.WriteLine($"CPU name: {cpu.Name}");
                //foreach (var core in cpu.Cores)
                //{
                    //Console.WriteLine($"{core.Id} : {core.Temperature}ºC, {core.Speed} MHz");
                //}
                //Console.WriteLine("*******************************");

                //await Task.Delay(TimeSpan.FromSeconds(5));
                counter++;
            }

            stopwatch.Stop();

            var miliseconds = stopwatch.ElapsedMilliseconds;

            // await tcpClient.Client.DisconnectAsync(false);

            tcpClient.Close();

            Console.WriteLine($"Needed {miliseconds} miliseconds");

            Console.WriteLine($"tcpClient.Connected {tcpClient.Connected}");

            await Task.CompletedTask;
        }

        public static async Task WaitForData(TcpClient tcpClient)
        {
            while(tcpClient.Available < 2)
            {
                await Task.Delay(TimeSpan.FromMilliseconds(10));
            }

            await Task.CompletedTask;
        }
    }
}