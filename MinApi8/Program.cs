using Microsoft.Extensions.FileProviders;
using MinApi8;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddSingleton<SimpleFileLogger>();

builder.Services.AddDirectoryBrowser();
var fileProvider = new PhysicalFileProvider(builder.Environment.WebRootPath);


builder.Services.AddDirectoryBrowser();

var app = builder.Build();

app.UseDirectoryBrowser(new DirectoryBrowserOptions
{
    FileProvider = fileProvider,
    RequestPath = "/wwwroot",
    
});



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

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
.WithDescription("p�edpov�� po�as�")
.WithOpenApi();

app.MapGet("/", () => $"API b��, nyn� je {DateTime.Now}").WithOpenApi();

app.MapGet("/detail/{id:int}", (int id, SimpleFileLogger logger) =>
    {
        logger.Log("loguji z api");
        return $"Zobrazuji detail produktu id: {id}";
    }
    );


//app.Map("/custom", (ILogger log) => Custom.Process(log));
app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
