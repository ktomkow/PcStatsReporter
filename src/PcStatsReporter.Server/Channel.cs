using System.Net.NetworkInformation;
using System.Net.Sockets;
using PcStatsReporter.Contracts;

namespace PcStatsReporter.Server;

public class Channel
{
    public Guid Id { get; }
    private string ShortId => Id.ToString().Substring(0, 4);
    private readonly TcpClient _tcpClient;
    public ChannelState State { get; private set; }

    public Channel(TcpClient tcpClient)
    {
        _tcpClient = tcpClient;
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
            while(_tcpClient.Available < 3 && _tcpClient.GetState() == TcpState.Established)
            {
                await Task.Delay(TimeSpan.FromMilliseconds(100));
            }
            
            while (stream.DataAvailable)
            {
                Byte[] buffer = new Byte[_tcpClient.Available];
                
                await stream.ReadAsync(buffer, 0, buffer.Length);
                
                var toServer = ToServer.Parser.ParseFrom(buffer);
                string data = toServer.MyMessage.Text;
            
                Console.WriteLine($"Id: {ShortId} - Message: {data}");
            }
        }

        Console.WriteLine($"{ShortId} Finished");
        State = ChannelState.Finished;
        await Task.CompletedTask;
    }
}