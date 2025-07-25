using RecipeShare;
using Serilog;
using Serilog.Extensions.Logging;
using Serilog.Formatting.Elasticsearch;

var builder = WebApplication.CreateBuilder(args);

builder.Host
    .ConfigureAppConfiguration((_, configurationBuilder) =>
    {
        var configuration = configurationBuilder.Build();
        ConfigureGlobalLogger(configuration);
    })
    .ConfigureLogging((_, logging) =>
    {
        logging.ClearProviders()
            .SetMinimumLevel(LogLevel.Debug);
    })
    .UseSerilog((context, configuration) =>
    {
        configuration.ReadFrom.Configuration(context.Configuration)
            .Enrich.FromLogContext()
            .WriteTo.Console(new ElasticsearchJsonFormatter());
    });

var startup = new Startup(builder.Configuration);

startup.ConfigureServices(builder.Services);

var app = builder.Build();

startup.Configure(app, app.Environment, 
    new SerilogLoggerFactory(Log.Logger).CreateLogger<Startup>());


app.Run();

static void ConfigureGlobalLogger(IConfiguration configuration)
{
    Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(configuration).Enrich.FromLogContext()
        .WriteTo.Console(new ElasticsearchJsonFormatter()).CreateBootstrapLogger();
}

// Partial Program class to allow for benchmarking
public partial class Program
{
    
}