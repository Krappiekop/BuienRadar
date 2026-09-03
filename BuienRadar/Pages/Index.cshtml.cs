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
    public string FoutMelding { get; set; }

    // Property waarin ik straks de opgehaalde JSON tekst opslaan, zodat de Razor pagina (Index.cshtml) deze via @Model.RuweData kan tonen.
    public BuienradarJSON Data { get; set; }

    // Constructor van IndexModel, wordt automatisch aangeroepen zodra ASP.NET Core een nieuwe IndexModel aanmaakt voor een bezoek aan de pagina.
    public IndexModel(IHttpClientFactory factory)
    {
        // Haalt de specifieke client op die we in Program.cs hadden geregistreerd onder de naam "json", inclusief de BaseAddress die daar is ingesteld.
        _client = factory.CreateClient("json");
    }

    public async Task OnGetAsync()
    {
        try
        {
            Data = await _client.GetFromJsonAsync<BuienradarJSON>("2.0/feed/json");
        }
        catch (HttpRequestException)
        {
            FoutMelding = "De gegevens konden niet worden opgehaald, probeer het later opnieuw.";
        }
    }
}
