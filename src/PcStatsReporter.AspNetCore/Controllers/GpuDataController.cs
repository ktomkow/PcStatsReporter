using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PcStatsReporter.AspNetCore.Mappers;
using PcStatsReporter.Core.Models;
using PcStatsReporter.LibreHardware;
using PcStatsReporter.RestContracts;

namespace PcStatsReporter.AspNetCore.Controllers;

/// <summary>
/// nVidia gpu related data
/// </summary>
[ApiController]
[Route("api/nvidia")]
public class GpuDataController : ControllerBase
{
    private readonly IMap<GpuData, GpuResponse> mapper;
    private readonly GpuDataCollector collector;

    public GpuDataController(IMap<GpuData, GpuResponse> mapper, GpuDataCollector collector)
    {
        this.mapper = mapper;
        this.collector = collector;
    }
    
    /// <summary>
    /// This method gets latest data about dedicated GPU
    /// </summary>
    /// <returns>GpuResponse</returns>
    /// /// <response code="200">Returns data</response>
    [HttpGet]
    [ProducesResponseType(typeof(GpuResponse),StatusCodes.Status200OK)]
    [Produces("application/json")]
    public IActionResult Get()
    {
        GpuData gpuData = collector.Collect();
        GpuResponse result = mapper.Map(gpuData); 
        
        return Ok(result);
    }
}