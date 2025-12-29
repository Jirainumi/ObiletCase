using System.Text.Json.Serialization;

namespace ObiletCase.Models.ResponseModels;

public class GetSessionResponse
{
    [JsonPropertyName("status")]
    public string Status { get; set; } = string.Empty;

    [JsonPropertyName("data")]
    public SessionData? Data { get; set; }

    [JsonPropertyName("message")]
    public string? Message { get; set; }

    [JsonPropertyName("user-message")]
    public string? UserMessage { get; set; }
}

public class SessionData
{
    [JsonPropertyName("session-id")]
    public string SessionId { get; set; } = string.Empty;

    [JsonPropertyName("device-id")]
    public string DeviceId { get; set; } = string.Empty;
}
