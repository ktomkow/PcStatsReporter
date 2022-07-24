namespace PcStatsReporter.Client.Initialization;

public abstract class Initializable : IInitializable
{
    protected TimeSpan _delayTime = TimeSpan.FromSeconds(1);
    
    public async Task WaitForInitialization()
    {
        while (await this.IsInitialized() == false)
        {
            await Wait();
        }

        await Task.CompletedTask;
    }

    public abstract Task<bool> IsInitialized();
    
    private async Task Wait()
    {
        await Task.Delay(_delayTime);
    }
}