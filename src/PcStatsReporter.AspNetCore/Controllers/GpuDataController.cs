using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PcStatsReporter.AspNetCore.Mappers;
using PcStatsReporter.Core.Maps;
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
    public GpuDataController()
    {

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
        GpuResponse result = new GpuResponse(); 
        
        return Ok(result);
    }
}