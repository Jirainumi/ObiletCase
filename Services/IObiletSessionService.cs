namespace ObiletCase.Services;

/// <summary>
/// Obilet session servis interface'i
/// </summary>
public interface IObiletSessionService
{
    Task<(string SessionId, string DeviceId)> GetOrCreateSessionAsync(HttpContext httpContext);
}

