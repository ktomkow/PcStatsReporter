using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PcStatsReporter.AspNetCore.Mappers;
using PcStatsReporter.Core.Models;
using PcStatsReporter.LibreHardware;
using PcStatsReporter.RestContracts;

namespace PcStatsReporter.AspNetCore.Controllers;

/// <summary>
/// RAM related data
/// </summary>
[ApiController]
[Route("api/ram")]
public class RamDataController : ControllerBase
{
    private readonly IMap<RamData, RamResponse> mapper;
    private readonly RamDataCollector collector;

    public RamDataController(IMap<RamData, RamResponse> mapper, RamDataCollector collector)
    {
        this.mapper = mapper;
        this.collector = collector;
    }
    
    /// <summary>
    /// This method gets latest data about RAM memory
    /// </summary>
    /// <returns>RamResponse</returns>
    /// /// <response code="200">Returns data</response>
    [HttpGet]
    [ProducesResponseType(typeof(CpuResponse),StatusCodes.Status200OK)]
    [Produces("application/json")]
    public IActionResult Get()
    {
        RamData ramData = collector.Collect();
        RamResponse result = mapper.Map(ramData); 
        
        return Ok(result);
    }
}