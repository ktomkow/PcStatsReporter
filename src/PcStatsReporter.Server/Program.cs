using System.Net;
using System.Net.Sockets;

namespace PcStatsReporter.Server
{
    public static class Program
    {
        public static async Task Main()
        {
            Console.WriteLine("Server Init");
            var tasks = new List<Task>();
            TcpListener server = new TcpListener(IPAddress.Parse("127.0.0.1"), 9090);

            server.Start();
            Console.WriteLine("Server has started on 127.0.0.1:9090.{0}Waiting for a connection...",
                Environment.NewLine);

            while (true)
            {
                TcpClient client = await server.AcceptTcpClientAsync();
                Foo foo = new(client);
                var task = foo.Run();
                tasks.Add(task);
            }
            
            await Task.CompletedTask;
        }
    }
}