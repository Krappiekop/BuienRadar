using Microsoft.EntityFrameworkCore;
using eWeather.Backend.Models;
public class DataUploadService : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly HttpClient _client;
    public DataUploadService(IServiceScopeFactory scopeFactory, IHttpClientFactory clientFactory)
    {
        _scopeFactory = scopeFactory;
        _client = clientFactory.CreateClient("json");
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<EWeatherContext>();
                

                var data = await _client.GetFromJsonAsync<BuienradarJSON>("2.0/feed/json");
                // Console.WriteLine($"Aantal weerstations opgehaald: {data.Actual.StationMeasurements.Count}");
                var nieuweMetingen = data.Actual.StationMeasurements.Select(s => new WeerMeting
                {
                    Tijdstip = DateTime.UtcNow,
                    Station = s.StationName,
                    Temperature = s.Temperature,
                    FeelTemperature = s.FeelTemperature,
                    GroundTemperature = s.GroundTemperature,
                    SunPower = s.SunPower,
                    RainFallLastHour = s.RainFallLastHour,
                    WindDirection = s.WindDirection
                }).ToList();
                // Console.WriteLine($"Aantal weerstations opgeslagen: {nieuweMetingen.Count}");
                context.WeerMetingen.AddRange(nieuweMetingen);
                await context.SaveChangesAsync();

                Console.WriteLine($"Aantal metingen in database: {context.WeerMetingen.Count()}");
            }

            await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
        }
    }
}


public class EWeatherContext : DbContext
{
    public DbSet<WeerMeting> WeerMetingen { get; set; }
    public EWeatherContext(DbContextOptions<EWeatherContext> options)
    : base(options)
    {
    }
}

public class WeerMeting
{
    public int Id { get; set; }
    public DateTime Tijdstip { get; set; }
    public string Station { get; set; }
    public float Temperature { get; set; }
    public float FeelTemperature { get; set; }
    public float GroundTemperature { get; set; }
    public float SunPower { get; set; }
    public float RainFallLastHour { get; set; }
    public string? WindDirection { get; set; }
}