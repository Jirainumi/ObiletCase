namespace ObiletCase.Constants;

/// <summary>
/// Kullanıcıya gösterilecek hata mesajları
/// </summary>
public static class ErrorMessages
{
    // Genel Hatalar
    public const string GeneralError = "Bir hata oluştu. Lütfen tekrar deneyin.";
    public const string Timeout = "İşlem zaman aşımına uğradı. Lütfen tekrar deneyin.";
    public const string NoConnection = "Bağlantı kurulamadı. İnternet bağlantınızı kontrol edin.";

    // Session Hataları
    public const string SessionCreateError = "Oturum oluşturulamadı. Lütfen sayfayı yenileyin.";
    public const string SessionExpired = "Oturumunuz sona erdi. Lütfen sayfayı yenileyin.";

    // Lokasyon Hataları
    public const string LocationsLoadError = "Lokasyonlar yüklenemedi. Lütfen tekrar deneyin.";
    public const string LocationNotFound = "Seçilen lokasyon bulunamadı.";

    // Sefer Hataları
    public const string JourneysLoadError = "Seferler yüklenemedi. Lütfen tekrar deneyin.";
    public const string NoJourneysFound = "Seçtiğiniz kriterlere uygun sefer bulunamadı.";
    public const string InvalidRoute = "Seçilen güzergahta sefer bulunmamaktadır.";
    public const string InvalidDate = "Geçersiz tarih seçimi. Bugün veya ileri bir tarih seçiniz.";

    // Validasyon Hataları
    public const string SameLocationError = "Kalkış ve varış noktası aynı olamaz.";
    public const string MissingFields = "Lütfen tüm alanları doldurun.";
    public const string PastDateError = "Geçmiş bir tarih seçilemez.";
}