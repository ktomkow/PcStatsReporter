using Microsoft.Extensions.Logging;

namespace PcStatsReporter.Client;

public class SettingsInitializer : Initializer<Settings>
{
    private readonly ClientChannel _clientChannel;

    public SettingsInitializer(ILogger<SettingsInitializer> logger, ClientChannel clientChannel) : base(logger)
    {
        _clientChannel = clientChannel;
    }

    protected override async Task InitializeResult(Settings initializable)
    {
        await _clientChannel.WaitForInitialization();
        initializable.MarkAsInitialized() ;
    }
}