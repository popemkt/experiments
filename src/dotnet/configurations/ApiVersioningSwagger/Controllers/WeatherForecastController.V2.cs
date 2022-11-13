
using Microsoft.AspNetCore.Mvc;

namespace ApiVersioningSwagger.Controllers;

[ApiController]
[ApiVersion("2")]
[Route("w")]
public class WeatherForecastControllerV2 : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastControllerV2> _logger;

    public WeatherForecastControllerV2(ILogger<WeatherForecastControllerV2> logger)
    {
        _logger = logger;
    }

    [MapToApiVersion("2")]
    [HttpGet(Name = "GetWeatherForecast")]
    public string GetV2()
    {
        return "v2";
    }
}