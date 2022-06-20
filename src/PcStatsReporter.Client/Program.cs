using System.Net;
using System.Net.Sockets;
using System.Text;
using Google.Protobuf;
using PcStatsReporter.Contracts;

namespace PcStatsReporter.Client
{
    public class Program
    {
        public static async Task Main()
        {
            Console.WriteLine("Client Init");

            TcpClient tcpClient = new TcpClient("127.0.0.1", 9090);

            Console.WriteLine($"tcpClient.Connected {tcpClient.Connected}");

            
            while (true)
            {
                var line = Console.ReadLine();

                for (int i = 0; i < 3; i++)
                {
                    var toServer = new ToServer();
                    toServer.MyMessage = new MyMessage();
                    toServer.MyMessage.Text = line;

                    var payload = toServer.ToByteArray();
                
                    await tcpClient.GetStream().WriteAsync(payload, 0, payload.Length);
                }
            }
            

            // await tcpClient.Client.DisconnectAsync(false);

            tcpClient.Close();

            Console.WriteLine($"tcpClient.Connected {tcpClient.Connected}");

            await Task.CompletedTask;
        }
    }
}