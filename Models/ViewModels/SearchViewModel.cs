using ObiletCase.Models.ResponseModels;

namespace ObiletCase.Models.ViewModels;

/// <summary>
/// Arama sayfası için view model
/// </summary>
public class SearchViewModel
{
    public List<BusLocation> Locations { get; set; } = new();
    public int? SelectedOriginId { get; set; }
    public int? SelectedDestinationId { get; set; }
    public DateTime SelectedDate { get; set; } = DateTime.Now.AddDays(1);
}

