namespace ObiletCase.Models.Configuration;

/// <summary>
/// Obilet API yapýlandýrma ayarlarý
/// appsettings.json'dan yüklenir
/// </summary>
public class ObiletApiSettings
{
    /// <summary>
    /// API base URL
    /// </summary>
    public string BaseUrl { get; set; } = string.Empty;

    /// <summary>
    /// API client authorization token
    /// </summary>
    public string ApiClientToken { get; set; } = string.Empty;
}