using Microsoft.AspNetCore.Mvc;
using Serilog;
using WeatherApp.Domain.Dto;
using WeatherApp.Domain.Interfaces;
using ILogger = Microsoft.Extensions.Logging.ILogger;
//using ILogger = Serilog.ILogger;

namespace WeatherAppApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly IWeatherManager _assetManager;

    private readonly Microsoft.Extensions.Logging.ILogger _logger;


    public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherManager assetManager)
    {
        _logger = logger;
        _assetManager = assetManager;
    }

    [HttpGet("{city}")]
    public async Task<List<Cities>> GetCities(string city)
    {
        _logger.LogInformation("GetCities fired on {date}", DateTime.Now);

        return await _assetManager.GetCities(city).ConfigureAwait(false);
    }

    [HttpGet("{latitude}/{longitude}")]
    public async Task<CityWeather> GetWeatherReport(decimal latitude, decimal longitude)
    {
        _logger.LogInformation("GetWeatherReport fired on {date}", DateTime.Now);
        return await _assetManager.GetWeatherReport(latitude, longitude).ConfigureAwait(false);
    }

}
