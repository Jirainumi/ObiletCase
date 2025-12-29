using ObiletCase.Models.RequestModels;
using ObiletCase.Models.ResponseModels;

namespace ObiletCase.Services;

/// <summary>
/// Otobüs lokasyon servis interface'i
/// </summary>
public interface IBusLocationService
{
    Task<List<BusLocation>> GetBusLocationsAsync(string searchText, string sessionId, string deviceId);
}

