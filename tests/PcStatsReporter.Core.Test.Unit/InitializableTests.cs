using System;
using System.Threading.Tasks;
using FluentAssertions;
using PcStatsReporter.Core.Initialization;
using Xunit;

namespace PcStatsReporter.Core.Test.Unit;

public class InitializableTests
{
    [Fact]
    public async Task Test()
    {
        var service = new Service();
        var firstClient = new Client(service);
        var secondClient = new Client(service);

        var firstClientTask = firstClient.Add(1, 2);
        var secondClientTask = secondClient.Add(10, 20);

        await Task.Delay(TimeSpan.FromMilliseconds(100));
        await service.Initialize().ConfigureAwait(false);

        var firstResult = await firstClientTask.ConfigureAwait(false);
        var secondResult = await secondClientTask.ConfigureAwait(false);

        firstResult.Should().Be(3);
        secondResult.Should().Be(30);
    }

    private class Service : Initializable
    {
        public Service()
        {
            _delayTime = TimeSpan.FromMilliseconds(1);
        }
        
        public override Task Initialize()
        {
            SetInitialized();
            return Task.CompletedTask;
        }
    }

    private class Client
    {
        private readonly Service _service;

        public Client(Service service)
        {
            _service = service;
        }

        public async Task<int> Add(int a, int b)
        {
            await _service.WaitForInitialization();
            return a + b;
        }
    }
}