using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PcStatsReporter.Core.Models;
using PcStatsReporter.Core.Persistence;
using PcStatsReporter.RestContracts;

namespace PcStatsReporter.AspNetCore.Controllers;

/// <summary>
/// RAM related data
/// </summary>
[ApiController]
[Route("api/ram")]
public class RamDataController : ControllerBase
{
    private readonly IHold _holder;

    public RamDataController(IHold holder)
    {
        _holder = holder;
    }
    
    /// <summary>
    /// This method gets latest data about RAM memory
    /// </summary>
    /// <returns>RamResponse</returns>
    /// <response code="200">Returns data</response>
    /// <response code="204">Data not available yet</response>
    [HttpGet]
    [ProducesResponseType(typeof(RamResponse),StatusCodes.Status200OK)]
    [Produces("application/json")]
    public async Task<IActionResult> Get()
    {
        var pcInfo = await _holder.Get<PcInfo>();
        var latestRamSample = await _holder.Get<RamSample>();

        if (pcInfo is null || latestRamSample is null)
        {
            return NoContent();
        }

        var result = new RamResponse();
        
        return Ok(result);
    }
}