var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSwaggerGen(options =>
{
    var httpContextAccessor = builder.Services.BuildServiceProvider().GetRequiredService<IHttpContextAccessor>();
    var request = httpContextAccessor?.HttpContext?.Request;
    var url = $"{request?.Scheme}://{request?.Host.Value}";
    options.AddServer(new Microsoft.OpenApi.Models.OpenApiServer() { Url = url });
});

builder.Services.AddAiPluginGen(options =>
{
    options.NameForHuman = "Weather Forecast";
    options.NameForModel = "weatherforecast";
    options.LegalInfoUrl = "https://example.com/legal";
    options.ContactEmail = "noreply@example.com";
    options.LogoUrl = "https://example.com/logo.png";
    options.DescriptionForHuman = "Search for weather forecasts";
    options.DescriptionForModel = "Plugin for searching the weather forecast. Use It whenever a users asks about weather or forecasts";
    options.ApiDefinition = new() { RelativeUrl = "/swagger/v1/swagger.yaml" };
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAiPluginGen();
app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

public partial class Program { }
