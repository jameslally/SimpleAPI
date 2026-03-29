using SimpleAPI.Models;

namespace SimpleAPI.Tests;

public class WeatherForecastTests
{
    [Fact]
    public void TemperatureF_ComputesFromCelsius()
    {
        var forecast = new WeatherForecast(DateOnly.MinValue, 0, "Cold");

        Assert.Equal(32, forecast.TemperatureF);
    }
}
