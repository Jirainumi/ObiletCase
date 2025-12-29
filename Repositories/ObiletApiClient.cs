using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Options;
using ObiletCase.Constants;
using ObiletCase.Models.Configuration;
using ObiletCase.Models.RequestModels;
using ObiletCase.Models.ResponseModels;

namespace ObiletCase.Repositories;

/// <summary>
/// Obilet API istemci implementasyonu
/// </summary>
public class ObiletApiClient : IObiletApiClient
{
    private readonly HttpClient _httpClient;
    private readonly ObiletApiSettings _settings;
    private readonly JsonSerializerOptions _jsonOptions;
    private readonly ILogger<ObiletApiClient> _logger;

    public ObiletApiClient(
        HttpClient httpClient, 
        IOptions<ObiletApiSettings> settings,
        ILogger<ObiletApiClient> logger)
    {
        _httpClient = httpClient;
        _settings = settings.Value;
        _logger = logger;
        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        // API base URL ayarı
        if (_httpClient.BaseAddress == null && !string.IsNullOrEmpty(_settings.BaseUrl))
        {
            _httpClient.BaseAddress = new Uri(_settings.BaseUrl);
        }

        // Authentication header ayarları
        if (!string.IsNullOrEmpty(_settings.ApiClientToken))
        {
            _httpClient.DefaultRequestHeaders.Authorization = 
                new AuthenticationHeaderValue("Basic", _settings.ApiClientToken);
        }
        _httpClient.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
    }

    public async Task<GetSessionResponse> GetSessionAsync(GetSessionRequest request)
    {
        try
        {
            var jsonContent = JsonSerializer.Serialize(request, _jsonOptions);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("/api/client/getsession", content);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<GetSessionResponse>(responseContent, _jsonOptions);

            return result ?? new GetSessionResponse 
            { 
                Status = "error", 
                Message = "Response deserialize edilemedi",
                UserMessage = ErrorMessages.SessionCreateError
            };
        }
        catch (TaskCanceledException ex) when (ex.InnerException is TimeoutException)
        {
            _logger.LogWarning(ex, "GetSession timeout");
            return new GetSessionResponse
            {
                Status = "error",
                Message = "Timeout",
                UserMessage = ErrorMessages.Timeout
            };
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "GetSession HTTP error");
            return new GetSessionResponse
            {
                Status = "error",
                Message = ex.Message,
                UserMessage = ErrorMessages.NoConnection
            };
        }
        catch (JsonException ex)
        {
            _logger.LogError(ex, "GetSession JSON parse error");
            return new GetSessionResponse
            {
                Status = "error",
                Message = $"JSON parse hatası: {ex.Message}",
                UserMessage = ErrorMessages.SessionCreateError
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "GetSession unexpected error");
            return new GetSessionResponse
            {
                Status = "error",
                Message = ex.Message,
                UserMessage = ErrorMessages.GeneralError
            };
        }
    }

    public async Task<GetBusLocationsResponse> GetBusLocationsAsync(
        GetBusLocationsRequest request, 
        string sessionId, 
        string deviceId)
    {
        try
        {
            if (string.IsNullOrEmpty(sessionId) || string.IsNullOrEmpty(deviceId))
            {
                _logger.LogWarning("GetBusLocations called with null session/device");
                return new GetBusLocationsResponse
                {
                    Status = "error",
                    Message = "Invalid session",
                    UserMessage = ErrorMessages.SessionExpired
                };
            }

            request.DeviceSession ??= new DeviceSession();
            request.DeviceSession.SessionId = sessionId;
            request.DeviceSession.DeviceId = deviceId;

            var jsonContent = JsonSerializer.Serialize(request, _jsonOptions);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("/api/location/getbuslocations", content);

            var responseContent = await response.Content.ReadAsStringAsync();
            
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning("GetBusLocations HTTP {StatusCode}: {Response}", 
                    response.StatusCode, responseContent);
                
                return new GetBusLocationsResponse
                {
                    Status = "error",
                    Message = $"HTTP {(int)response.StatusCode}",
                    UserMessage = ErrorMessages.LocationsLoadError
                };
            }

            var result = JsonSerializer.Deserialize<GetBusLocationsResponse>(responseContent, _jsonOptions);
            return result ?? new GetBusLocationsResponse 
            { 
                Status = "error", 
                Message = "Response deserialize edilemedi",
                UserMessage = ErrorMessages.LocationsLoadError
            };
        }
        catch (TaskCanceledException ex) when (ex.InnerException is TimeoutException)
        {
            _logger.LogWarning(ex, "GetBusLocations timeout");
            return new GetBusLocationsResponse
            {
                Status = "error",
                Message = "Timeout",
                UserMessage = ErrorMessages.Timeout
            };
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "GetBusLocations HTTP error");
            return new GetBusLocationsResponse
            {
                Status = "error",
                Message = ex.Message,
                UserMessage = ErrorMessages.NoConnection
            };
        }
        catch (JsonException ex)
        {
            _logger.LogError(ex, "GetBusLocations JSON parse error");
            return new GetBusLocationsResponse
            {
                Status = "error",
                Message = $"JSON parse hatası: {ex.Message}",
                UserMessage = ErrorMessages.LocationsLoadError
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "GetBusLocations unexpected error");
            return new GetBusLocationsResponse
            {
                Status = "error",
                Message = ex.Message,
                UserMessage = ErrorMessages.GeneralError
            };
        }
    }

    public async Task<GetJourneysResponse> GetJourneysAsync(
        GetJourneysRequest request, 
        string sessionId, 
        string deviceId)
    {
        try
        {
            if (string.IsNullOrEmpty(sessionId) || string.IsNullOrEmpty(deviceId))
            {
                _logger.LogWarning("GetJourneys called with null session/device");
                return new GetJourneysResponse
                {
                    Status = "error",
                    Message = "Invalid session",
                    UserMessage = ErrorMessages.SessionExpired
                };
            }

            request.DeviceSession.SessionId = sessionId;
            request.DeviceSession.DeviceId = deviceId;

            var jsonContent = JsonSerializer.Serialize(request, _jsonOptions);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("/api/journey/getbusjourneys", content);
            
            var responseContent = await response.Content.ReadAsStringAsync();
            
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning("GetJourneys HTTP {StatusCode}: {Response}", 
                    response.StatusCode, responseContent);
                
                return new GetJourneysResponse
                {
                    Status = "error",
                    Message = $"HTTP {(int)response.StatusCode}",
                    UserMessage = ErrorMessages.JourneysLoadError
                };
            }

            var result = JsonSerializer.Deserialize<GetJourneysResponse>(responseContent, _jsonOptions);
            
            if (result != null)
            {
                // API'den gelen spesifik hataları kontrol et
                if (result.Status == "InvalidRoute")
                {
                    result.UserMessage = ErrorMessages.InvalidRoute;
                }
                else if (result.Status == "InvalidDepartureDate")
                {
                    result.UserMessage = ErrorMessages.InvalidDate;
                }
                else if (result.Status == "InvalidLocation")
                {
                    result.UserMessage = ErrorMessages.LocationNotFound;
                }
            }
            
            return result ?? new GetJourneysResponse 
            { 
                Status = "error", 
                Message = "Response deserialize edilemedi",
                UserMessage = ErrorMessages.JourneysLoadError
            };
        }
        catch (TaskCanceledException ex) when (ex.InnerException is TimeoutException)
        {
            _logger.LogWarning(ex, "GetJourneys timeout");
            return new GetJourneysResponse
            {
                Status = "error",
                Message = "Timeout",
                UserMessage = ErrorMessages.Timeout
            };
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "GetJourneys HTTP error");
            return new GetJourneysResponse
            {
                Status = "error",
                Message = ex.Message,
                UserMessage = ErrorMessages.NoConnection
            };
        }
        catch (JsonException ex)
        {
            _logger.LogError(ex, "GetJourneys JSON parse error");
            return new GetJourneysResponse
            {
                Status = "error",
                Message = $"JSON parse hatası: {ex.Message}",
                UserMessage = ErrorMessages.JourneysLoadError
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "GetJourneys unexpected error");
            return new GetJourneysResponse
            {
                Status = "error",
                Message = ex.Message,
                UserMessage = ErrorMessages.GeneralError
            };
        }
    }
}
