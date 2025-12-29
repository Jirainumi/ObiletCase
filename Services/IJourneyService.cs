using ObiletCase.Models.ResponseModels;

namespace ObiletCase.Services;

/// <summary>
/// Sefer servis interface'i
/// </summary>
public interface IJourneyService
{
    Task<List<Journey>> GetJourneysAsync(int originId, int destinationId, DateTime date, string sessionId, string deviceId);
}

