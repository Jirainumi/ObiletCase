using System.Text.Json.Serialization;

namespace ObiletCase.Models.RequestModels;

public class GetSessionRequest
{
    [JsonPropertyName("type")]
    public int Type { get; set; } = 1; // 1=web

    [JsonPropertyName("connection")]
    public Connection Connection { get; set; } = new();

    [JsonPropertyName("browser")]
    public Browser Browser { get; set; } = new();

    // Bazý ortamlarda application da istenebiliyor; koymak güvenli
    [JsonPropertyName("application")]
    public Application Application { get; set; } = new();
}

public class Connection
{
    [JsonPropertyName("ip-address")]
    public string IpAddress { get; set; } = "127.0.0.1";

    [JsonPropertyName("port")]
    public string Port { get; set; } = "5117";
}

public class Browser
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = "Chrome";

    [JsonPropertyName("version")]
    public string Version { get; set; } = "120.0.0.0";
}

public class Application
{
    [JsonPropertyName("version")]
    public string Version { get; set; } = "120.0.0.0";

    [JsonPropertyName("equipment-id")]
    public string EquipmentId { get; set; } = "distribution";
}
