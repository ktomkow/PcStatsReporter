using System;
using System.Threading.Tasks;
using Grpc.Net.Client;
using PcStatsReporter.Grpc.Proto;

namespace PcStatsReporter.GrpcClient;

public static class Program
{
    public static async Task Main()
    {
        Console.WriteLine("Init");

        using var channel = GrpcChannel.ForAddress("http://localhost:5287");

        var client = new Calculator.CalculatorClient(channel);

        var input = new Numbers()
        {
            Info = "My message",
            Number = {1, 2, 3, 4, 5, 6, 7, 8, 9}
        };

        Console.WriteLine($"I'm asking for {input.Info} - {string.Join(", ", input.Number)}");

        var reply = await client.CalculateAsync(input);

        Console.WriteLine($"I got answer: {reply.Sum_}");

        Console.WriteLine("Done");
    }
}