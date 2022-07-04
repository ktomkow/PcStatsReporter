using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PcStatsReporter.AspNetCore.Mappers;
using PcStatsReporter.Core.Models;
using PcStatsReporter.LibreHardware;
using PcStatsReporter.RestContracts;

namespace PcStatsReporter.AspNetCore.Controllers;

/// <summary>
/// CPU related data
/// </summary>
[ApiController]
[Route("api/cpu")]
public class CpuDataController : ControllerBase
{
    private readonly CpuDataCollector cpuDataCollector;
    private readonly IMap<CpuData, CpuResponse> mapper;

    public CpuDataController(CpuDataCollector cpuDataCollector, IMap<CpuData, CpuResponse> mapper)
    {
        this.cpuDataCollector = cpuDataCollector;
        this.mapper = mapper;
    }

    /// <summary>
    /// This method gets latest data about CPU temperatures, speed and load. It also includes CPU name
    /// </summary>
    /// <returns>CpuResponse</returns>
    /// /// <response code="200">Returns data</response>
    [HttpGet]
    [ProducesResponseType(typeof(CpuResponse),StatusCodes.Status200OK)]
    [Produces("application/json")]
    public IActionResult Get()
    {
        var cpuData = cpuDataCollector.Collect();

        CpuResponse result = mapper.Map(cpuData); 
        
        return Ok(result);
    }
}