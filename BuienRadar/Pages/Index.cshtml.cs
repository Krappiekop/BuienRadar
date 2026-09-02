using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BuienRadar.Pages;

public class IndexModel : PageModel
{
    private readonly HttpClient _client;
    public string RuweData { get; set; }

    public IndexModel(IHttpClientFactory factory)
    {
        _client = factory.CreateClient("demo");
    }

    public async Task OnGetAsync()
    {
        RuweData = await _client.GetStringAsync("posts/1");
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
