using System.Text.Json.Serialization;

namespace ObiletCase.Models.RequestModels;

/// <summary>
/// Obilet GetJourneys API request modeli
/// </summary>
public class GetJourneysRequest
{
    [JsonPropertyName("device-session")]
    public DeviceSession DeviceSession { get; set; } = new();

    [JsonPropertyName("date")]
    public string Date { get; set; } = DateTime.Now.ToString("yyyy-dd-MM'T'HH:mm:ss");

    [JsonPropertyName("language")]
    public string Language { get; set; } = "tr-TR";

    [JsonPropertyName("data")]
    public JourneyData Data { get; set; } = new();
}

public class DeviceSession
{
    [JsonPropertyName("session-id")]
    public string SessionId { get; set; } = string.Empty;

    [JsonPropertyName("device-id")]
    public string DeviceId { get; set; } = string.Empty;
}

public class JourneyData
{
    [JsonPropertyName("origin-id")]
    public int OriginId { get; set; }

    [JsonPropertyName("destination-id")]
    public int DestinationId { get; set; }

    [JsonPropertyName("departure-date")]
    public string DepartureDate { get; set; } = string.Empty;
}

