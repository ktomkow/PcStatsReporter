﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using PcStatsReporter.AspNetCore.SignalR.Contracts;
using PcStatsReporter.Core.Maps;
using PcStatsReporter.Core.Models;
using PcStatsReporter.Core.Persistence;

namespace PcStatsReporter.AspNetCore.SignalR;

public class ReporterHub : Hub, IReporterHub
{
    private readonly IHold _hold;
    private readonly IMap<PcInfo, PcInfoDto> _pcInfoMap;

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

        await Clients.All.SendAsync("registerBasicData", dto);
    }

    public Task SendCpuData()
    {
        throw new System.NotImplementedException();
    }
}