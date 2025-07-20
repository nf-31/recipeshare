using RecipeShare;
using RecipeShare.ExtensionMethods;

var builder = WebApplication.CreateBuilder(args);

builder.Host.AddSerilogConfiguration();

var startup = new Startup(builder.Configuration);

startup.ConfigureServices(builder.Services);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

startup.Configure(app, app.Environment);


app.Run();

