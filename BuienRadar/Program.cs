var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// Registreert een HttpClient met de naam "json" bij de dependency injection container.
// Overal in de applicatie kan ik deze specifieke client straks ophalen met IHttpClientFactory.CreateClient("json")
builder.Services.AddHttpClient("json", client => 
{ 
    // BaseAddress is het vaste basisdeel van de url.
    // Bij elke aanroep vanuit deze client geef ik straks alleen nog het resterende pad mee,
    // bijvoorbeeld "2.0/feed/json", en dat wordt automatisch achter deze BaseAddress geplakt.
    client.BaseAddress = new Uri("https://data.buienradar.nl/"); 
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
