using NLog.Web;
using Slot.Application.Configuration;
using Slot.Application.Configuration.Middlewares;
using Slot.Application.Hubs;
using Slot.Infrastructure;
using Slot.Infrastructure.Db;

namespace Slot.API;

public class Program
{
    public static async Task Main(string[] args)
    {
        var logger = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
        try
        {
            logger.Info("Initializing the application...");

            var builder = WebApplication.CreateBuilder(args);

            builder.Logging.ClearProviders();
            builder.Host.UseNLog();

            IConfiguration configuration = builder.Configuration;

            builder.Configuration
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true);

            builder.Services.AddControllers();

            builder.Services.AddSignalR();

            builder.Services.AddApiLayer(builder.Configuration, builder.Environment)
                            .AddApplicationLayer()
                            .AddInfrastructureLayer(builder.Configuration)
                            .AddCustomRepositories(configuration);

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger()
                   .UseSwaggerUI();
            }

            app.ConfigureApiLayer(configuration);

            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<SlotDbContext>();
                if (context != null)
                {
                    await DBInitializer.InitializeDatabase(app.Services, context);
                }
            }

            app.UseHttpsRedirection()
               .UseMiddleware<ErrorHandlerMiddleware>()
               .UseRouting()
               .UseAuthentication()
               .UseAuthorization()
               .UseEndpoints(endpoints => { endpoints.MapControllers(); });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<SlotHub>("/slot-hub");
            });

            await app.RunAsync();
        }
        catch (Exception ex)
        {
            logger.Error(ex, "Stopped program because of an exception.");
            throw;
        }
        finally
        {
            NLog.LogManager.Shutdown();
        }
    }
}
