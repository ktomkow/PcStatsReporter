using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PcStatsReporter.AspNetCore.Mappers;
using PcStatsReporter.Core.Maps;
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
    public RamDataController()
    {

    }
    
    /// <summary>
    /// This method gets latest data about RAM memory
    /// </summary>
    /// <returns>RamResponse</returns>
    /// /// <response code="200">Returns data</response>
    [HttpGet]
    [ProducesResponseType(typeof(RamResponse),StatusCodes.Status200OK)]
    [Produces("application/json")]
    public IActionResult Get()
    {
        RamResponse result = new RamResponse();
        
        return Ok(result);
    }
}