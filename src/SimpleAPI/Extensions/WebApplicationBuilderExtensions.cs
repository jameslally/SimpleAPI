using SimpleAPI.Services;

namespace SimpleAPI.Extensions;

public static class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder AddSimpleApi(this WebApplicationBuilder builder)
    {
        builder.Services.AddOpenApi();
        builder.Services.AddSingleton<IWeatherForecastService, WeatherForecastService>();
        return builder;
    }
}
