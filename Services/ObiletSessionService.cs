using ObiletCase.Models.RequestModels;
using ObiletCase.Models.ResponseModels;
using ObiletCase.Repositories;

namespace ObiletCase.Services;

/// <summary>
/// Obilet session yönetim servisi
/// </summary>
public class ObiletSessionService : IObiletSessionService
{
    private readonly IObiletApiClient _apiClient;
    private const string SessionKey = "Obilet_SessionId";
    private const string DeviceKey = "Obilet_DeviceId";

    public ObiletSessionService(IObiletApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    public async Task<(string SessionId, string DeviceId)> GetOrCreateSessionAsync(HttpContext httpContext)
    {
        // Session'dan mevcut bilgileri kontrol et
        var sessionId = httpContext.Session.GetString(SessionKey);
        var deviceId = httpContext.Session.GetString(DeviceKey);

        // Eğer session varsa ve geçerliyse geri döndür
        if (!string.IsNullOrEmpty(sessionId) && !string.IsNullOrEmpty(deviceId))
        {
            return (sessionId, deviceId);
        }

        // Yeni session oluştur
        var request = new GetSessionRequest();
        var response = await _apiClient.GetSessionAsync(request);

        if (response.Status == "Success" && response.Data != null)
        {
            sessionId = response.Data.SessionId;
            deviceId = response.Data.DeviceId;

            // Session'a kaydet
            httpContext.Session.SetString(SessionKey, sessionId);
            httpContext.Session.SetString(DeviceKey, deviceId);
        }
        else
        {
            throw new Exception(response.UserMessage ?? response.Message ?? "Session oluşturulamadı");
        }

        return (sessionId, deviceId);
    }
}

