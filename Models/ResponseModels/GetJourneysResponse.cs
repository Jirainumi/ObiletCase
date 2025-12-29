using ObiletCase.Helpers;
using System.Text.Json.Serialization;

namespace ObiletCase.Models.ResponseModels;

/// <summary>
/// Obilet GetJourneys API response modeli
/// </summary>
public class GetJourneysResponse
{
    [JsonPropertyName("status")]
    public string Status { get; set; } = string.Empty;

    [JsonPropertyName("data")]
    public List<Journey>? Data { get; set; }

    [JsonPropertyName("message")]
    public string? Message { get; set; }

    [JsonPropertyName("user-message")]
    public string? UserMessage { get; set; }

    [JsonPropertyName("api-request-id")]
    public string? ApiRequestId { get; set; }

    [JsonPropertyName("controller")]
    public string? Controller { get; set; }
}

public class Journey
{
    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("partner-id")]
    public int PartnerId { get; set; }

    [JsonPropertyName("partner-name")]
    public string PartnerName { get; set; } = string.Empty;

    [JsonPropertyName("route-id")]
    public int RouteId { get; set; }

    [JsonPropertyName("bus-type")]
    public string BusType { get; set; } = string.Empty;

    [JsonPropertyName("bus-type-name")]
    public string? BusTypeName { get; set; }

    [JsonPropertyName("total-seats")]
    public int TotalSeats { get; set; }

    [JsonPropertyName("available-seats")]
    public int AvailableSeats { get; set; }

    [JsonPropertyName("journey")]
    public JourneyDetails JourneyInfo { get; set; } = new();    

    [JsonPropertyName("features")]
    public List<JourneyFeature>? Features { get; set; }

    [JsonPropertyName("origin-location")]
    public string OriginLocation { get; set; } = string.Empty;

    [JsonPropertyName("destination-location")]
    public string DestinationLocation { get; set; } = string.Empty;

    [JsonPropertyName("is-active")]
    public bool IsActive { get; set; }

    [JsonPropertyName("origin-location-id")]
    public int OriginLocationId { get; set; }

    [JsonPropertyName("destination-location-id")]
    public int DestinationLocationId { get; set; }

    [JsonPropertyName("is-promoted")]
    public bool IsPromoted { get; set; }

    [JsonPropertyName("cancellation-offset")]
    public int? CancellationOffset { get; set; }

    [JsonPropertyName("has-bus-shuttle")]
    public bool HasBusShuttle { get; set; }

    [JsonPropertyName("disable-sales-without-gov-id")]
    public bool? DisableSalesWithoutGovId { get; set; }

    [JsonPropertyName("display-offset")]
    public string? DisplayOffset { get; set; }

    [JsonPropertyName("partner-rating")]
    public decimal? PartnerRating { get; set; }
}

public class JourneyDetails
{
    [JsonPropertyName("kind")]
    public string Kind { get; set; } = string.Empty;

    [JsonPropertyName("code")]
    public string Code { get; set; } = string.Empty;

    [JsonPropertyName("stops")]
    public List<JourneyStop>? Stops { get; set; }

    [JsonPropertyName("origin")]
    public string Origin { get; set; } = string.Empty;

    [JsonPropertyName("destination")]
    public string Destination { get; set; } = string.Empty;

    [JsonPropertyName("departure")]
    public DateTime Departure { get; set; }

    [JsonPropertyName("arrival")]
    public DateTime Arrival { get; set; }

    [JsonPropertyName("currency")]
    public string Currency { get; set; } = string.Empty;

    [JsonPropertyName("duration")]
    public string Duration { get; set; } = string.Empty;

    [JsonPropertyName("original-price")]
    public decimal OriginalPrice { get; set; }

    [JsonPropertyName("internet-price")]
    public decimal InternetPrice { get; set; }

    [JsonPropertyName("provider-internet-price")]
    public decimal? ProviderInternetPrice { get; set; }

    [JsonPropertyName("booking")]
    public List<object>? Booking { get; set; }

    [JsonPropertyName("bus-name")]
    public string? BusName { get; set; }

    [JsonPropertyName("policy")]
    public JourneyPolicy? Policy { get; set; }

    [JsonPropertyName("features")]
    public List<string>? Features { get; set; }

    [JsonPropertyName("features-description")]
    public string? FeaturesDescription { get; set; }

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("available")]
    public object? Available { get; set; }

    [JsonPropertyName("partner-provider-code")]
    public string? PartnerProviderCode { get; set; }

    [JsonPropertyName("peron-no")]
    public string? PeronNo { get; set; }

    [JsonPropertyName("partner-provider-id")]
    public int? PartnerProviderId { get; set; }

    [JsonPropertyName("is-segmented")]
    public bool? IsSegmented { get; set; }

    [JsonPropertyName("partner-name")]
    public string? PartnerName { get; set; }

    [JsonPropertyName("provider-code")]
    public string? ProviderCode { get; set; }

    [JsonPropertyName("sorting-price")]
    public decimal? SortingPrice { get; set; }

    [JsonPropertyName("has-multiple-brandedfare-selection")]
    public bool? HasMultipleBrandedfareSelection { get; set; }

    [JsonPropertyName("has-available-seat-info")]
    public bool? HasAvailableSeatInfo { get; set; }

    [JsonPropertyName("duration-offset")]
    public string? DurationOffset { get; set; }

    [JsonPropertyName("service-fee")]
    public decimal? ServiceFee { get; set; }

    [JsonPropertyName("should-set-seats-to-zero")]
    public bool? ShouldSetSeatsToZero { get; set; }
}

public class JourneyStop
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("station")]
    [JsonConverter(typeof(FlexibleIntConverter))]  // ?? Converter eklendi
    public int? Station { get; set; }

    [JsonPropertyName("time")]
    public DateTime? Time { get; set; }

    [JsonPropertyName("is-origin")]
    public bool IsOrigin { get; set; }

    [JsonPropertyName("is-destination")]
    public bool IsDestination { get; set; }

    // Ek alanlar
    [JsonPropertyName("id")]
    public int? Id { get; set; }

    [JsonPropertyName("kolayCarLocationId")]
    public int? KolayCarLocationId { get; set; }

    [JsonPropertyName("is-segment-stop")]
    public bool? IsSegmentStop { get; set; }

    [JsonPropertyName("index")]
    public int? Index { get; set; }

    [JsonPropertyName("obilet-station-id")]
    public int? ObiletStationId { get; set; }

    [JsonPropertyName("map-url")]
    public string? MapUrl { get; set; }

    [JsonPropertyName("station-phone")]
    public string? StationPhone { get; set; }

    [JsonPropertyName("station-address")]
    public string? StationAddress { get; set; }

    [JsonPropertyName("tz-code")]
    public string? TzCode { get; set; }
}

public class JourneyPolicy
{
    [JsonPropertyName("max-seats")]
    public int? MaxSeats { get; set; }

    [JsonPropertyName("max-single")]
    public int? MaxSingle { get; set; }

    [JsonPropertyName("max-single-males")]
    public int? MaxSingleMales { get; set; }

    [JsonPropertyName("max-single-females")]
    public int? MaxSingleFemales { get; set; }

    [JsonPropertyName("mixed-genders")]
    public bool MixedGenders { get; set; }

    [JsonPropertyName("gov-id")]
    public bool GovId { get; set; }

    [JsonPropertyName("lht")]
    public bool Lht { get; set; }
}

public class JourneyFeature
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("priority")]
    public byte? Priority { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("is-promoted")]
    public bool IsPromoted { get; set; }

    [JsonPropertyName("back-color")]
    public string? BackColor { get; set; }

    [JsonPropertyName("fore-color")]
    public string? ForeColor { get; set; }
}