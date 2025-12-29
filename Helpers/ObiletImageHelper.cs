namespace ObiletCase.Helpers;

/// <summary>
/// Obilet API için resim URL'lerini oluþturan yardýmcý sýnýf
/// </summary>
public static class ObiletImageHelper
{
    private const string BaseImageUrl = "https://s3.eu-central-1.amazonaws.com/static.obilet.com/images";

    /// <summary>
    /// Partner (firma) logosunun URL'ini döndürür
    /// </summary>
    /// <param name="partnerId">Partner ID</param>
    /// <param name="size">Logo boyutu (sm, md, lg). Varsayýlan: sm</param>
    /// <returns>Logo URL'i</returns>
    public static string GetPartnerLogoUrl(int partnerId, string size = "sm")
    {
        return $"{BaseImageUrl}/partner/{partnerId}-{size}.png";
    }

    /// <summary>
    /// Özellik (feature) ikonunun URL'ini döndürür
    /// </summary>
    /// <param name="featureId">Feature ID</param>
    /// <returns>Ýkon URL'i</returns>
    public static string GetFeatureIconUrl(int featureId)
    {
        return $"{BaseImageUrl}/feature/{featureId}.svg";
    }
}