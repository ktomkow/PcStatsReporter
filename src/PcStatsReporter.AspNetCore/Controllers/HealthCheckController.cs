using Microsoft.AspNetCore.Mvc;

namespace PcStatsReporter.AspNetCore.Controllers;

[ApiController]
[Route("api/hc")]
public class HealthCheckController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return NoContent();
    }
}