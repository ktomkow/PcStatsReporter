﻿using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

    public CpuDataController(IHold holder)
    {
        _holder = holder;
    }

    /// <summary>
    /// This method gets latest data about CPU temperatures, speed and load. It also includes CPU name
    /// </summary>
    /// <returns>CpuResponse</returns>
    /// <response code="200">Returns data</response>
    /// <response code="204">Request is handled properly but no data is available yet</response>
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
            AverageLoad = latestCpuSample.AverageLoad,
            PackageTemperature = latestCpuSample.Temperature,
            Cores = latestCpuSample.Cores.Select(x => new CpuCoreResponse()
            {
                Id = x.CoreNumber,
                Speed = x.Speed,
                Temperature = x.Temperature,
                Load = x.ThreadsLoad.Select(y => y.threadLoad).ToList()
            }).ToList()
        };

        return Ok(result);
    }
}