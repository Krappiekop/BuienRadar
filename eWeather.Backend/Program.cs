using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<EWeatherContext>(options =>
    options.UseSqlite("Data Source=eweather.db"));

builder.Services.AddHostedService<DataUploadService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weerdata", (DateOnly StartDate, DateOnly? EndDate, string Station = "Eindhoven") =>
{
    var effectiveEndDate = EndDate ?? StartDate.AddDays(7);

    var forecast =
        new StationMeting
        (
            StartDate,
            effectiveEndDate,
            Station,
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        )
        ;
    return forecast;
})
.WithName("GetWeerData");

app.Run();

record StationMeting(DateOnly Startdate, DateOnly EndDate, string Station, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}


