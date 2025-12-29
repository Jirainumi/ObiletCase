using ObiletCase.Models.RequestModels;
using ObiletCase.Models.ResponseModels;
using ObiletCase.Repositories;

namespace ObiletCase.Services;

/// <summary>
/// Sefer servisi
/// </summary>
public class JourneyService : IJourneyService
{
    private readonly IObiletApiClient _apiClient;

    public JourneyService(IObiletApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    public async Task<List<Journey>> GetJourneysAsync(int originId, int destinationId, DateTime date, string sessionId, string deviceId)
    {
        var request = new GetJourneysRequest
        {
            DeviceSession = new Models.RequestModels.DeviceSession
            {
                SessionId = sessionId,
                DeviceId = deviceId
            },
            Date = date.ToString("yyyy-MM-dd'T'HH:mm:ss"),
            Language = "tr-TR",
            Data = new JourneyData
            {
                OriginId = originId,
                DestinationId = destinationId,
                DepartureDate = date.ToString("yyyy-MM-dd'T'00:00:00")
            }
        };

        var response = await _apiClient.GetJourneysAsync(request, sessionId, deviceId);

        if (response.Status == "Success" && response.Data != null)
        {
            // Departure time'a göre artan sırada sırala
            return response.Data
                .Where(j => j.JourneyInfo?.Departure != null)
                .OrderBy(j => j.JourneyInfo!.Departure)
                .ToList();
        }

        return new List<Journey>();
    }
}

