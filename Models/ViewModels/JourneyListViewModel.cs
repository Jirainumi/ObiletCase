using ObiletCase.Models.ResponseModels;

namespace ObiletCase.Models.ViewModels;

/// <summary>
/// Sefer listesi sayfası için view model
/// </summary>
public class JourneyListViewModel
{
    public List<Journey> Journeys { get; set; } = new();
    public BusLocation? OriginLocation { get; set; }
    public BusLocation? DestinationLocation { get; set; }
    public DateTime JourneyDate { get; set; }
}

