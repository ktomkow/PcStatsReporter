using Microsoft.Extensions.Logging;

namespace PcStatsReporter.Client;

public abstract class Initializer<T> : IInitializer<T> where T : IInitializable
{
    protected readonly ILogger _logger;
    protected readonly SemaphoreSlim _semaphore;
    
    public T Initialized { get; protected set; }

    protected Initializer(ILogger logger)
    {
        _logger = logger;
        _semaphore = new SemaphoreSlim(1);
    }
    
    protected abstract Task InitializeResult(T initializable);
    
    public async Task Initialize(T initializable)
    {
        await _semaphore.WaitAsync();
        
        if (await initializable.IsInitialized())
        { 
            _semaphore.Release();
        }
        
        _logger.LogInformation($"Initializing object");
        
        await InitializeResult(initializable);
        
        _logger.LogInformation($"Object initialized");

        _semaphore.Release();
    }
}