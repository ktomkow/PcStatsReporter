using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using PcStatsReporter.AspNetCore.SignalR.Contracts;
using PcStatsReporter.Core.Maps;
using PcStatsReporter.Core.Models;
using PcStatsReporter.Core.Persistence;
using Rebus.Handlers;

namespace PcStatsReporter.AspNetCore.SignalR;

// public class ReporterHub : Hub, IReporterHub
public class ReporterHub : Hub
{
    private readonly IHold _hold;
    private readonly IMap<PcInfo, PcInfoDto> _pcInfoMap;

    public const string CpuCollectionMethod = "handleCpuData";
    public const string GpuCollectionMethod = "handleGpuData";
    public const string RamCollectionMethod = "handleRamData";
    
    public const string RegisterMethod = "registerBasicData";
    // await _hub.Clients.All.SendAsync("handleCpuData", dto);

    public ReporterHub(IHold hold, IMap<PcInfo, PcInfoDto> pcInfoMap)
    {
        _hold = hold;
        _pcInfoMap = pcInfoMap;
    }
    
    public override async Task OnConnectedAsync()
    {
        var pcInfo = await _hold.Get<PcInfo>() ?? new PcInfo()
        {
            CpuName = "unknown cpu",
            GpuName = "unknown gpu",
            TotalRam = 0
        };

        var dto = _pcInfoMap.Map(pcInfo);

        await Clients.All.SendAsync(RegisterMethod, dto);
    }
}