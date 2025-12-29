using System.Text.Json.Serialization;

public class GetBusLocationsRequest
{
    [JsonPropertyName("data")]
    public object? Data { get; set; }

    [JsonPropertyName("device-session")]
    public DeviceSession DeviceSession { get; set; } = new();

    [JsonPropertyName("date")]
    public string Date { get; set; } = string.Empty;

    [JsonPropertyName("language")]
    public string Language { get; set; } = "tr-TR";
}

public class DeviceSession
{
    [JsonPropertyName("session-id")]
    public string SessionId { get; set; } = string.Empty;

    [JsonPropertyName("device-id")]
    public string DeviceId { get; set; } = string.Empty;
}
