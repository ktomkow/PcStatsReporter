using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;

namespace PcStatsReporter.Server;

public class Foo
{
    public Guid Id { get; }
    private string ShortId => Id.ToString().Substring(0, 4);
    private readonly TcpClient _tcpClient;
    public FooState State { get; private set; }

    public Foo(TcpClient tcpClient)
    {
        _tcpClient = tcpClient;
        State = FooState.Created;
        Id = Guid.NewGuid();
    }

    public async Task Run()
    {
        if (State != FooState.Created)
        {
            await Task.CompletedTask;
            return;
        }
        
        Console.WriteLine($"{ShortId} Started");

        State = FooState.Started;

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
                string data = Encoding.UTF8.GetString(buffer);
            
                Console.WriteLine($"Id: {ShortId} - Message: {data}");
            }
        }

        Console.WriteLine($"{ShortId} Finished");
        State = FooState.Finished;
        await Task.CompletedTask;
    }
}