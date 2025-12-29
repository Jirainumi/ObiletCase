using Microsoft.Extensions.Caching.Memory;
using ObiletCase.Models.RequestModels;
using ObiletCase.Models.ResponseModels;
using ObiletCase.Repositories;

namespace ObiletCase.Services;

/// <summary>
/// Otobüs lokasyon servisi
/// </summary>
public class BusLocationService : IBusLocationService
{
    private readonly IObiletApiClient _apiClient;
    private readonly IMemoryCache _cache;

    public BusLocationService(IObiletApiClient apiClient, IMemoryCache cache)
    {
        _apiClient = apiClient;
        _cache = cache;
    }

    public async Task<List<BusLocation>> GetBusLocationsAsync(string searchText, string sessionId, string deviceId)
    {
        // Cache key oluştur
        var cacheKey = $"bus_locations_{searchText?.ToLower() ?? "all"}";

        // Cache'den kontrol et
        if (_cache.TryGetValue(cacheKey, out List<BusLocation>? cachedLocations) && cachedLocations != null)
        {
            return cachedLocations;
        }

        // API'den çek
        var request = new GetBusLocationsRequest
        {
            // searchText boşsa null gönder, doluysa string gönder
            Data = string.IsNullOrWhiteSpace(searchText) ? null : searchText,

            DeviceSession = new DeviceSession
            {
                SessionId = sessionId,
                DeviceId = deviceId
            },
            Date = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss"),
            Language = "tr-TR"
        };

        var response = await _apiClient.GetBusLocationsAsync(request, sessionId, deviceId);

        if (response.Status == "Success" && response.Data != null)
        {
            var locations = response.Data;

            // Cache'e kaydet (5 dakika)
            _cache.Set(cacheKey, locations, TimeSpan.FromMinutes(5));

            return locations;
        }

        return new List<BusLocation>();
    }
}

