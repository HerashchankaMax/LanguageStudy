using Serilog;

namespace LangCardsAPI.Services;

public static class ProjectService
{
    public static WebApplicationBuilder ConfigureLogger(this WebApplicationBuilder builder)
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .Enrich.FromLogContext()
            .Enrich.WithProperty("Application", "LangCardsAPI")
            .WriteTo.Console()
            .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();
        builder.Host.UseSerilog();
        return builder;
    }
}