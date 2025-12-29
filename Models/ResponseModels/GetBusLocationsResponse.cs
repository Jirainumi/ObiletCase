using System.Text.Json.Serialization;

namespace ObiletCase.Models.ResponseModels;

/// <summary>
/// Obilet GetBusLocations API response modeli
/// </summary>
public class GetBusLocationsResponse
{
    [JsonPropertyName("status")]
    public string Status { get; set; } = string.Empty;

    [JsonPropertyName("data")]
    public List<BusLocation>? Data { get; set; }

    [JsonPropertyName("message")]
    public string? Message { get; set; }

    [JsonPropertyName("user-message")]
    public string? UserMessage { get; set; }

    [JsonPropertyName("api-request-id")]
    public string? ApiRequestId { get; set; }

    [JsonPropertyName("controller")]
    public string? Controller { get; set; }
}

public class BusLocation
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("parent-id")]
    public int? ParentId { get; set; }

    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("geo-location")]
    public GeoLocation? GeoLocation { get; set; }

    [JsonPropertyName("tz-code")]
    public string? TzCode { get; set; }

    [JsonPropertyName("weather-code")]
    public string? WeatherCode { get; set; }

    [JsonPropertyName("rank")]
    public int? Rank { get; set; }

    [JsonPropertyName("reference-code")]
    public string? ReferenceCode { get; set; }

    [JsonPropertyName("city-id")]
    public int? CityId { get; set; }

    [JsonPropertyName("city-name")]
    public string? CityName { get; set; }

    [JsonPropertyName("keywords")]
    public string? Keywords { get; set; }
}

public class GeoLocation
{
    [JsonPropertyName("latitude")]
    public double Latitude { get; set; }

    [JsonPropertyName("longitude")]
    public double Longitude { get; set; }

    [JsonPropertyName("zoom")]
    public int Zoom { get; set; } 
}
