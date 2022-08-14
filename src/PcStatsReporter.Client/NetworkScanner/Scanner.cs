﻿using Microsoft.Extensions.Logging;

namespace PcStatsReporter.Client.NetworkScanner;

public class Scanner
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<Scanner> _logger;

    public Scanner(IHttpClientFactory  httpClientFactory, ILogger<Scanner> logger)
    {
        _httpClientFactory = httpClientFactory;
        _logger = logger;
    }
    
    public async Task<string> Scan(int port)
    {
        HttpClient httpClient = _httpClientFactory.CreateClient();

        string host = "";
        for (int i = 0; i < 1; i++)
        {
            // dummy but works
            // TODO: change it
            // host = $"http://192.168.0.{i}";
            string endpoint = $":{port}/api/hc";
            host = "http://localhost";
            _logger.LogDebug("Checking {Host}", host);
            var cts = new CancellationTokenSource(250);
            var token = cts.Token;

            try
            {
                await httpClient.GetAsync(host + endpoint, token);

                _logger.LogDebug("Host {Host} is GOOD", host);
                return host;
            }
            catch (TaskCanceledException e)
            {
                _logger.LogDebug(e, "Host {Host} is not valid", host);
            }
            finally
            {
                cts.Dispose();
            }
        }

        throw new Exception("Service not found");
    }
}