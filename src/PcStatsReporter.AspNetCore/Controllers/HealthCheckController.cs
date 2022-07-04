using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PcStatsReporter.AspNetCore.Controllers;

/// <summary>
/// Health-check
/// </summary>
[ApiController]
[Route("api/hc")]
public class HealthCheckController : ControllerBase
{
    /// <summary>
    /// Check if application works
    /// </summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult Get()
    {
        return NoContent();
    }
}