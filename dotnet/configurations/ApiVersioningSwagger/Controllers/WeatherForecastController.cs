using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers;

[ApiController]
[ApiVersion("1")]
[Route("w")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [MapToApiVersion("1")]
    [HttpGet(Name = "GetWeatherForecast")]
    public string Get()
    {
        return "v1";
    }
}