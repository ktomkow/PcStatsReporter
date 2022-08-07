using System;
using System.Threading.Tasks;

namespace PcStatsReporter.Core.Initializable;

public abstract class Initializable : IInitializable
{
    protected TimeSpan _delayTime = TimeSpan.FromSeconds(1);
    
    private object _lock = new object();
    private bool _isInitialized = false;

    public abstract Task Initialize();

    public async Task WaitForInitialization()
    {
        while (this.IsInitialized() == false)
        {
            await Wait();
        }

        await Task.CompletedTask;
    }

    public virtual bool IsInitialized()
    {
        lock (_lock)
        {
            return _isInitialized;
        }
    }

    protected virtual bool SetInitialized()
    {
        lock (_lock)
        {
            return _isInitialized = true;
        }
    }
    
    private async Task Wait()
    {
        await Task.Delay(_delayTime);
    }
}