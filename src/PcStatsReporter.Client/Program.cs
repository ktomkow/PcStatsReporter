using System.Net.Sockets;
using System.Text;

namespace PcStatsReporter.Client
{
    public static class Program
    {
        public static async Task Main()
        {
            Console.WriteLine("Client Init");

            TcpClient tcpClient = new TcpClient("127.0.0.1", 9090);

            Console.WriteLine($"tcpClient.Connected {tcpClient.Connected}");


            while (true)
            {
                var line = Console.ReadLine();

                byte[] payload = Encoding.UTF8.GetBytes(line);

                await tcpClient.GetStream().WriteAsync(payload, 0, payload.Length);
            }


            // await tcpClient.Client.DisconnectAsync(false);

            tcpClient.Close();

            Console.WriteLine($"tcpClient.Connected {tcpClient.Connected}");

            await Task.CompletedTask;
        }
    }
}