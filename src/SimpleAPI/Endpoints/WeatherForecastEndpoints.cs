using SimpleAPI.Services;

namespace SimpleAPI.Endpoints;

public static class WeatherForecastEndpoints
{
    public static IEndpointRouteBuilder MapWeatherForecastEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/", () => Results.Redirect("/weatherforecast"))
            .ExcludeFromDescription();

        app.MapGet("/weatherforecast", (IWeatherForecastService service) =>
                Results.Ok(service.GetForecast()))
            .WithName("GetWeatherForecast");

        return app;
    }
}
