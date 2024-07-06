using Microsoft.AspNetCore.Localization;
using System.Globalization;

namespace Slot.API;

public static class ServiceConfiguration
{
    public static IApplicationBuilder ConfigureApiLayer(this IApplicationBuilder app, IConfiguration configuration)
    {
        ConfigureCors(app, configuration);
        ConfigureLocalization(app);

        return app;
    }

    public static void ConfigureCors(this IApplicationBuilder app, IConfiguration configuration)
    {
        var origins = configuration.GetValue<string>("OriginsToAllow");

        if (origins != null)
        {
            app.UseCors(x => x
                .WithOrigins(origins.Split(";"))
                .AllowAnyMethod()
                .AllowCredentials()
                .AllowAnyHeader());
        }
        else
        {
            app.UseCors(x => x
                .WithOrigins("*")
                .AllowAnyMethod()
                .AllowCredentials()
                .AllowAnyHeader());
        }
    }

    public static void ConfigureLocalization(this IApplicationBuilder app)
    {

        var supportedCultures = new List<CultureInfo>
            {
                new CultureInfo("ka-GE"),
                new CultureInfo("en-US"),
            };

        var options = new RequestLocalizationOptions
        {
            DefaultRequestCulture = new RequestCulture("ka-GE"),
            SupportedCultures = supportedCultures,
            SupportedUICultures = supportedCultures
        };

        app.UseRequestLocalization(options);
    }
}
