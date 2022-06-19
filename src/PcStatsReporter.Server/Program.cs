using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;

namespace PcStatsReporter.Server
{
    public static class Program
    {
        public static async Task Main()
        {
            Console.WriteLine("Server Init");
            TcpListener server = new TcpListener(IPAddress.Parse("127.0.0.1"), 9090);
            
            server.Start();
            Console.WriteLine("Server has started on 127.0.0.1:9090.{0}Waiting for a connection...", Environment.NewLine);
            
            TcpClient client = await server.AcceptTcpClientAsync();
            Console.WriteLine($"1 client.GetState: {client.GetState()}");

            Console.WriteLine("A client connected.");

            var stream = client.GetStream();

            Console.WriteLine($"stream.Socket.Connected {stream.Socket.Connected}");
            Console.WriteLine($"client.Client.Connected: {client.Client.Connected}");

            // while (client.Client.Connected)
            while (client.GetState() == TcpState.Established)
            {
                Console.WriteLine("Inside first while");
                Console.WriteLine($"client.GetState: {client.GetState()}");
                while(client.Available < 3 && client.GetState() == TcpState.Established)
                {

                    await Task.Delay(TimeSpan.FromSeconds(5));
                    Console.WriteLine($"client.Available: {client.Available}");
                    Console.WriteLine($"client.Connected: {client.Connected}");
                    Console.WriteLine($"client.Client.Connected: {client.Client.Connected}");
                    Console.WriteLine($"client.GetState: {client.GetState()}");
                    Console.WriteLine($"Waiting for data to be available. {DateTime.UtcNow.Second}");
                }

                await Task.Delay(TimeSpan.FromMilliseconds(50));
                
                while (stream.DataAvailable)
                {
                    Console.WriteLine("Inside second while");
                    Byte[] buffer = new Byte[client.Available];
                    
                    await stream.ReadAsync(buffer, 0, buffer.Length);
                    string data = Encoding.UTF8.GetString(buffer);
                
                    Console.WriteLine($"data {data}");
                }

            }

            Console.WriteLine("Client disconnected");
            
            await Task.CompletedTask;
        }
    }
}