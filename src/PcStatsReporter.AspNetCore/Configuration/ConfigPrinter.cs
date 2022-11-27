using System;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace PcStatsReporter.AspNetCore.Configuration;

public class ConfigPrinter : IConfigPrinter
{
    private readonly ILogger<ConfigPrinter> _logger;
    private readonly IConfiguration _configuration;

    public ConfigPrinter(ILogger<ConfigPrinter> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }

    public void Print()
    {
        _logger.LogInformation("***************** Configuration *****************");
        
        var webSettingTypes = Assembly
            .GetExecutingAssembly()
            .GetTypes()
            .Where(x => x.IsAbstract == false)
            .Where(x => x.IsAssignableTo(typeof(IWebSettings)))
            .ToList();
        
        foreach (var settings in webSettingTypes)
        {
            object? instance = Activator.CreateInstance(settings);
            if (instance is null)
            {
                continue;
            }
            
            var sectionName = settings.Name.Replace("Settings", string.Empty);
            var section = _configuration.GetSection(sectionName);
            
            section.Bind(instance);

            JsonSerializerOptions serializerOptions = new()
            {
                WriteIndented = true
            };
                
            var json = JsonSerializer.Serialize(instance, serializerOptions);
            _logger.LogInformation("Section {Section} : {Settings}", sectionName, json);
        }
        
        _logger.LogInformation("*************************************************");
    }
}