# eWeather (Buienalarm opdracht #1)

Een ASP.NET Core Razor Pages applicatie die realtime weergegevens toont via de open data feed van Buienradar. De gebruiker kiest een weerstation en ziet de actuele metingen van dat station.

## Gekozen oplossing en aanpak
- Razor Pages in plaats van MVC met Controllers, omdat de app maar een paar simpele schermen nodig heeft
- Een `HttpClient`, geregistreerd via `IHttpClientFactory` in `Program.cs`, haalt de JSON feed op
- De JSON structuur is vertaald naar C# modelklassen (`BuienradarJSON`, `Actual`, `Stationmeasurement`) in de map `Models`, met `JsonPropertyName` attributen om de veldnamen te koppelen
- De gebruiker kiest een weerstation uit een dropdown, het formulier stuurt de keuze door via een GET request, en de pagina toont de gegevens van het gekozen station in een tabel

## Screenshots


## Vereisten om te bouwen
- .NET SDK 10.0 of hoger
- Een internetverbinding, de app haalt live data op bij `https://data.buienradar.nl/2.0/feed/json`
- Geen database of extra installaties nodig

## Dependencies en externe tools
- ASP.NET Core Razor Pages (onderdeel van de .NET SDK, geen losse NuGet package)
- FontAwesome, ingeladen via een CDN link in `_Layout.cshtml` (geen lokale installatie nodig)
- Buienradar open data feed, `https://data.buienradar.nl/2.0/feed/json`, gebruikt onder de voorwaarden van Buienradar/RTL

## Hoe bouw en start je het project
```bash
git clone https://github.com/gebruikersnaam/buienalarm.git
cd buienalarm
dotnet restore
dotnet run
```

De applicatie is daarna bereikbaar op de url die in de terminal getoond wordt, meestal `https://localhost:xxxx`.

## Bronvermelding
Weergegevens zijn afkomstig van [Buienradar.nl](https://www.buienradar.nl/), gebruikt onder de voorwaarden van de gratis weerdata feed.

## To do

### Al gedaan
- [x] Razor pages project opgezet en werkend gemaakt
- [x] Versiebeheer ingericht en project naar een eigen (privé) repository gezet
- [x] Verbinding met een externe API voorbereid in het project
- [x] Datamodellen gemaakt die aansluiten op de structuur van de opgehaalde data
- [x] Modellen overzichtelijk georganiseerd in een eigen map/namespace
- [x] Data ophalen en verwerken vanuit de API werkend gemaakt
- [x] Alle stations tonen in een simpele lijst, als eerste test
- [x] Dropdown gemaakt met alle weerstations, gekoppeld aan de juiste eigenschap
- [x] Geselecteerd station filteren en de gegevens overzichtelijk tonen in een tabel
- [x] Handmatige refresh knop toevoegen (los van of in aanvulling op de "Toon weerstation" knop)
- [x] Foutafhandeling: nette melding als de API niet bereikbaar is (try/catch rond de HttpClient aanroep)
- [x] Foutafhandeling: nette melding als er geen station geselecteerd is of geen match gevonden wordt
- [x] FontAwesome iconen toevoegen per meetwaarde (thermometer, druppel, kompas, en dergelijke)
- [x] Bronvermelding met hyperlink naar buienradar.nl zichtbaar maken in de applicatie zelf, bijvoorbeeld in de footer

### Nog te doen
- [x] Styling toepassen volgens het kleurenpalet (`#4ad6ed`, `#ffed00`, `#000000`, `#ffffff`)
- [ ] Fonts instellen (Helvetica Neue voor koppen, Arial voor platte tekst)
- [ ] Layout op laten lijken op de mockup (desktop en mobiel)
