using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BuienRadar.Models;

namespace BuienRadar.Pages;

public class IndexModel : PageModel
{
    // Veld om de HttpClient in te bewaren nadat we hem uit de constructor hebben gehaald.
    private readonly HttpClient _client;

    [BindProperty(SupportsGet = true)]
    public string GekozenWeerStation { get; set; }

    // Property waarin ik straks de opgehaalde JSON tekst opslaan, zodat de Razor pagina (Index.cshtml) deze via @Model.RuweData kan tonen.
    public BuienradarJSON Data { get; set; }

    // Constructor van IndexModel, wordt automatisch aangeroepen zodra ASP.NET Core een nieuwe IndexModel aanmaakt voor een bezoek aan de pagina.
    public IndexModel(IHttpClientFactory factory)
    {
        // Haalt de specifieke client op die we in Program.cs hadden geregistreerd onder de naam "json", inclusief de BaseAddress die daar is ingesteld.
        _client = factory.CreateClient("json");
    }

    // Wordt automatisch uitgevoerd bij een normaal bezoek (GET) aan de pagina. async omdat we hierin gaan wachten op een netwerkaanroep, zonder de rest van de applicatie te blokkeren.
    public async Task OnGetAsync()
    {
        // Doet een GET request naar BaseAddress + dit pad, dus in dit geval
        // https://data.buienradar.nl/ + 2.0/feed/json.
        // GetStringAsync geeft de ruwe response terug als platte tekst, zonder deserialisatie naar een class.
        Data = await _client.GetFromJsonAsync<BuienradarJSON>("2.0/feed/json");
    }
}