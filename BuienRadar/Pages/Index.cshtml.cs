using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BuienRadar.Pages;

public class IndexModel : PageModel
{
    // Veld om de HttpClient in te bewaren nadat we hem uit de constructor hebben gehaald.
    // private zodat alleen deze klasse erbij kan, readonly zodat hij na de constructor niet meer kan veranderen.
    private readonly HttpClient _client;

    // Property waarin ik straks de opgehaalde JSON tekst opslaan,
    // zodat de Razor pagina (Index.cshtml) deze via @Model.RuweData kan tonen.
    public string RuweData { get; set; }

    // Constructor van IndexModel, wordt automatisch aangeroepen zodra ASP.NET Core
    // een nieuwe IndexModel aanmaakt voor een bezoek aan de pagina.
    // IHttpClientFactory wordt automatisch meegegeven door dependency injection,
    // omdat ik die eerder in Program.cs beschikbaar hebben gemaakt.
    public IndexModel(IHttpClientFactory factory)
    {
        // Haalt de specifieke client op die we in Program.cs hadden geregistreerd onder de naam "json",
        // inclusief de BaseAddress die daar is ingesteld.
        _client = factory.CreateClient("json");
    }

    // Wordt automatisch uitgevoerd bij een normaal bezoek (GET) aan de pagina.
    // async omdat we hierin gaan wachten op een netwerkaanroep, zonder de rest van de applicatie te blokkeren.
    public async Task OnGetAsync()
    {
        // Doet een GET request naar BaseAddress + dit pad, dus in dit geval
        // https://data.buienradar.nl/ + 2.0/feed/json.
        // GetStringAsync geeft de ruwe response terug als platte tekst, zonder deserialisatie naar een class.
        RuweData = await _client.GetStringAsync("2.0/feed/json");
    }
}


public class BuienradarJSON
{
    public Actual Actual { get; set; }
}

public class Actual
{
    public List<Stationmeasurement> stationmeasurements { get; set; }
}

public class Stationmeasurement
{
    public string stationname { get; set; }
    public string regio { get; set; }
    public float temperature { get; set; }
    public float feeltemperature { get; set; }
    public float groundtemperature { get; set; }
    public float sunpower { get; set; }
    public float rainFallLastHour { get; set; }
    public string winddirection { get; set; }

}
