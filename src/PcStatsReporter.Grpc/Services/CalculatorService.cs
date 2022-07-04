using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using PcStatsReporter.Grpc.Proto;

namespace PcStatsReporter.Grpc.Services;

public class CalculatorService : Calculator.CalculatorBase
{
    // private readonly ILogger<CalculatorService> logger;
    //
    // public CalculatorService(ILogger<CalculatorService> logger)
    // {
    //     this.logger = logger;
    // }

    public override Task<Sum> Calculate(Numbers request, ServerCallContext context)
    {
        // logger.LogInformation("Handling Calculate Init");
        // logger.LogInformation(request.Info);
        uint sum = 0;
        foreach (var number in request.Number)
        {
            sum += number;
        }
        
        // logger.LogInformation("Handling Calculate Done");
        
        return Task.FromResult(new Sum()
        {
            Sum_ = sum
        });
    }
}