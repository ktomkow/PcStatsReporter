using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PcStatsReporter.AspNetCore.Mappers;
using PcStatsReporter.Core.Maps;
using PcStatsReporter.Core.Messages;
using PcStatsReporter.Core.Models;
using PcStatsReporter.LibreHardware;
using PcStatsReporter.RestContracts;
using Rebus.Bus;

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
    private readonly IBus _bus;

    public CpuDataController(CpuDataCollector cpuDataCollector, IMap<CpuData, CpuResponse> mapper, IBus bus)
    {
        this.cpuDataCollector = cpuDataCollector;
        this.mapper = mapper;
        _bus = bus;
    }

    /// <summary>
    /// This method gets latest data about CPU temperatures, speed and load. It also includes CPU name
    /// </summary>
    /// <returns>CpuResponse</returns>
    /// /// <response code="200">Returns data</response>
    [HttpGet]
    [ProducesResponseType(typeof(CpuResponse),StatusCodes.Status200OK)]
    [Produces("application/json")]
    public async Task<IActionResult> Get()
    {
        var registered = new Registered();
        await _bus.Publish(registered);
        
        var cpuData = cpuDataCollector.Collect();

        CpuResponse result = mapper.Map(cpuData); 
        
        return Ok(result);
    }
}