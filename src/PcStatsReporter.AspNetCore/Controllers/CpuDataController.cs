using Microsoft.AspNetCore.Mvc;
using PcStatsReporter.AspNetCore.Mappers;
using PcStatsReporter.Core.Models;
using PcStatsReporter.LibreHardware;
using PcStatsReporter.RestContracts;

namespace PcStatsReporter.AspNetCore.Controllers;

[ApiController]
[Route("api/cpu")]
public class CpuDataController : ControllerBase
{
    private readonly CpuDataCollector cpuDataCollector;
    private readonly IMap<CpuData, CpuResponse> mapper;

    public CpuDataController(CpuDataCollector cpuDataCollector, IMap<CpuData, CpuResponse> mapper)
    {
        this.cpuDataCollector = cpuDataCollector;
        this.mapper = mapper;
    }

    [HttpGet]
    public IActionResult Get()
    {
        var cpuData = cpuDataCollector.Collect();

        var result = mapper.Map(cpuData); 
        
        return Ok(result);
    }
}