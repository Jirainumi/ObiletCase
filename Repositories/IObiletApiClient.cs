using ObiletCase.Models.RequestModels;
using ObiletCase.Models.ResponseModels;

namespace ObiletCase.Repositories;

/// <summary>
/// Obilet API istemci interface'i
/// </summary>
public interface IObiletApiClient
{
    Task<GetSessionResponse> GetSessionAsync(GetSessionRequest request);
    Task<GetBusLocationsResponse> GetBusLocationsAsync(GetBusLocationsRequest request, string sessionId, string deviceId);
    Task<GetJourneysResponse> GetJourneysAsync(GetJourneysRequest request, string sessionId, string deviceId);
}

