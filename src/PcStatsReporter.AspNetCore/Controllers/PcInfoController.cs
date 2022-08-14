using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PcStatsReporter.Core.Models;
using PcStatsReporter.Core.Persistence;
using PcStatsReporter.RestContracts;

namespace PcStatsReporter.AspNetCore.Controllers;

/// <summary>
/// Pc info data
/// </summary>
[ApiController]
[Route("api/pc")]
public class PcInfoController : ControllerBase
{
    private readonly IHold _holder;

    public PcInfoController(IHold holder)
    {
        _holder = holder;
    }
    
    /// <summary>
    /// This method gets stable data about PC
    /// </summary>
    /// <returns>PcInfoResponse</returns>
    /// /// <response code="200">Returns data</response>
    [HttpGet]
    [ProducesResponseType(typeof(PcInfoResponse),StatusCodes.Status200OK)]
    [Produces("application/json")]
    public async Task<IActionResult> Get()
    {
        var pcInfo = await _holder.Get<PcInfo>();

        if (pcInfo is null)
        {
            return NoContent();
        }

        var result = new PcInfoResponse();
        
        return Ok(result);
    }
}