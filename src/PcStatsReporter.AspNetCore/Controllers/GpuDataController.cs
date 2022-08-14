using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PcStatsReporter.Core.Models;
using PcStatsReporter.Core.Persistence;
using PcStatsReporter.RestContracts;

namespace PcStatsReporter.AspNetCore.Controllers;

/// <summary>
/// nVidia gpu related data
/// </summary>
[ApiController]
[Route("api/nvidia")]
public class GpuDataController : ControllerBase
{
    private readonly IHold _holder;

    public GpuDataController(IHold holder)
    {
        _holder = holder;
    }

    /// <summary>
    /// This method gets latest data about dedicated GPU
    /// </summary>
    /// <returns>GpuResponse</returns>
    /// /// <response code="200">Returns data</response>
    [HttpGet]
    [ProducesResponseType(typeof(GpuResponse), StatusCodes.Status200OK)]
    [Produces("application/json")]
    public async Task<IActionResult> Get()
    {
        var pcInfo = await _holder.Get<PcInfo>();
        var latestGpuSample = await _holder.Get<GpuSample>();

        if (pcInfo is null || latestGpuSample is null)
        {
            return NoContent();
        }

        GpuResponse result = new GpuResponse();

        return Ok(result);
    }
}