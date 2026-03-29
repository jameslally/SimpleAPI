using Scalar.AspNetCore;
using SimpleAPI.Endpoints;

namespace SimpleAPI.Extensions;

public static class WebApplicationExtensions
{
    public static WebApplication ConfigureSimpleApi(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.MapScalarApiReference(options => options.DisableAgent());
        }

        var urls = Environment.GetEnvironmentVariable("ASPNETCORE_URLS");
        var hasHttpsUrl = urls?.Contains("https://", StringComparison.OrdinalIgnoreCase) == true;
        var hasHttpsPort = !string.IsNullOrEmpty(app.Configuration["ASPNETCORE_HTTPS_PORT"])
            || !string.IsNullOrEmpty(app.Configuration["HTTPS_PORTS"]);

        if (!app.Environment.IsEnvironment("Testing") && (hasHttpsUrl || hasHttpsPort))
        {
            app.UseHttpsRedirection();
        }

        app.MapWeatherForecastEndpoints();
        return app;
    }
}
