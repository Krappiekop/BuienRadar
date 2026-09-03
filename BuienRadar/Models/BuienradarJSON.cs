using System.Text.Json.Serialization;

namespace BuienRadar.Models;


public class BuienradarJSON
{
    [JsonPropertyName("actual")]
    public Actual Actual { get; set; }
}

public class Actual
{
    [JsonPropertyName("stationmeasurements")]
    public List<Stationmeasurement> StationMeasurements { get; set; }
}

public class Stationmeasurement
{
    [JsonPropertyName("stationname")]
    public string StationName { get; set; }

    [JsonPropertyName("regio")]
    public string Regio { get; set; }

    [JsonPropertyName("temperature")]
    public float Temperature { get; set; }

    [JsonPropertyName("feeltemperature")]
    public float FeelTemperature { get; set; }

    [JsonPropertyName("groundtemperature")]
    public float GroundTemperature { get; set; }

    [JsonPropertyName("sunpower")]
    public float SunPower { get; set; }

    [JsonPropertyName("rainFallLastHour")]
    public float RainFallLastHour { get; set; }

    [JsonPropertyName("winddirection")]
    public string WindDirection { get; set; }

}
