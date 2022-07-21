using System.Net.Sockets;

namespace PcStatsReporter.Client.NetworkScanner;

public class Scanner
{
    public async Task<string> Scan(int port)
    {
        using TcpClient tcpClient = new TcpClient();
        for (int i = 0; i < 256; i++)
        {
            try
            {
                string host = $"192.168.0.{i}";
                Console.WriteLine(host);
                CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
                CancellationToken cancellationToken = cancellationTokenSource.Token;
                Console.WriteLine(cancellationToken.IsCancellationRequested);

                ValueTask scanTask = tcpClient.ConnectAsync(host, port, cancellationToken);
                Task timeoutTask = Timeout(TimeSpan.FromMilliseconds(2000), cancellationToken);
                
                await Task.WhenAny(new Task[] {scanTask.AsTask(), timeoutTask});
                cancellationTokenSource.Cancel();
                
                if (timeoutTask.IsCompleted && !scanTask.IsCompleted)
                {
                    throw new Exception("Not really this address");
                }

                Console.WriteLine("YES");
                return host;
            }
            catch (Exception e)
            {
                // ignored
            }
        }

        throw new Exception("Not found");
    }

    private async Task Timeout(TimeSpan timeSpan, CancellationToken cancellationToken)
    {
        Console.WriteLine(cancellationToken.IsCancellationRequested);
        await Task.Delay(timeSpan, cancellationToken);
    }
}