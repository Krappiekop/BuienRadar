using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<EWeatherContext>(options =>
    options.UseSqlite("Data Source=eweather.db"));

builder.Services.AddHostedService<DataUploadService>();

builder.Services.AddHttpClient("json", client => 
{ 
    // BaseAddress is het vaste basisdeel van de url.
    // Bij elke aanroep vanuit deze client geef ik straks alleen nog het resterende pad mee,
    // bijvoorbeeld "2.0/feed/json", en dat wordt automatisch achter deze BaseAddress geplakt.
    client.BaseAddress = new Uri("https://data.buienradar.nl/"); 
});

builder.Services.Configure<WeerAPIOpties>(builder.Configuration.GetSection(nameof(WeerAPIOpties)));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();




app.MapGet("/weerdata", (DateOnly StartDate, DateOnly? EndDate, EWeatherContext context, string Station) =>
{
    var effectiveEndDate = EndDate ?? StartDate.AddDays(7);
    DateTime beginGrens = StartDate.ToDateTime(TimeOnly.MinValue);
    DateTime eindGrens = effectiveEndDate.AddDays(1).ToDateTime(TimeOnly.MinValue);

    var WeergaveGegevens = context.WeerMetingen
        .Where(m => m.Station == Station
            && m.Tijdstip >= beginGrens
            && m.Tijdstip < eindGrens)
        .OrderBy(m => m.Tijdstip)
        .ToList();
    return WeergaveGegevens;
})
.WithName("GetWeerData");

app.Run();




