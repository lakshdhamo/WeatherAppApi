using Serilog;
using WeatherApp.Domain.Helper;
using WeatherApp.Domain.Interfaces;
using WeatherApp.Domain.ServiceManager;
using WeatherApp.Domain.ServiceManager.CachingManager;
using WeatherApp.Domain.ServiceManager.LoggingManager;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddMemoryCache();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IWeatherManager, WeatherManager>();
builder.Services.AddScoped<ILogService, LogService>();
builder.Services.AddSingleton<ICacheManager, InMemoryCacheManager>();

// Register Serilog configuration
var logger = new LoggerConfiguration()
  .ReadFrom.Configuration(builder.Configuration)
  .Enrich.FromLogContext()
  .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

}

app.UseHttpsRedirection();
app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
app.UseAuthorization();
app.MapControllers();

//Constant Values
IConfiguration config = app.Services.GetRequiredService<IConfiguration>();
Constants.ForecastDays = config.GetValue<int>("ForecastDays");
Constants.ApiKey = config.GetValue<string>("ApiKey");
Constants.NoOfHours = config.GetValue<int>("NoOfHours");

app.Run();
