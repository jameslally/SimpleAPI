using SimpleAPI.Models;

namespace SimpleAPI.Services;

public interface IWeatherForecastService
{
    IReadOnlyList<WeatherForecast> GetForecast();
}
