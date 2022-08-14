using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PcStatsReporter.AspNetCore.SignalR.Contracts;
using PcStatsReporter.Core.Maps;
using PcStatsReporter.Core.Models;
using PcStatsReporter.Core.Persistence;
using PcStatsReporter.RestContracts;

namespace PcStatsReporter.AspNetCore.Controllers;

/// <summary>
/// CPU related data
/// </summary>
[ApiController]
[Route("api/cpu")]
public class CpuDataController : ControllerBase
{
    private readonly IHold _holder;
    private readonly IMap<CpuSample, CpuSampleDto> _map;

    public CpuDataController(IHold holder, IMap<CpuSample, CpuSampleDto> map)
    {
        _holder = holder;
        _map = map;
    }

    /// <summary>
    /// This method gets latest data about CPU temperatures, speed and load. It also includes CPU name
    /// </summary>
    /// <returns>CpuResponse</returns>
    /// /// <response code="200">Returns data</response>
    /// /// <response code="204">Request is handled properly but no data is available yet</response>
    /// /// <response code="500">Oups</response>
    [HttpGet]
    [ProducesResponseType(typeof(CpuResponse), StatusCodes.Status200OK)]
    [Produces("application/json")]
    public async Task<IActionResult> Get()
    {
        var pcInfo = await _holder.Get<PcInfo>();
        var latestCpuSample = await _holder.Get<CpuSample>();

        if (pcInfo is null || latestCpuSample is null)
        {
            return NoContent();
        }

        // todo: full map in separate file
        CpuResponse result = new CpuResponse()
        {
            Name = pcInfo.CpuName,
            Cores = new List<CpuCoreResponse>(),
            AverageLoad = latestCpuSample.AverageLoad,
            PackageTemperature = latestCpuSample.Temperature
        };

        return Ok(result);
    }
}