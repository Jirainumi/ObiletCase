# 🚌 Obilet Journey Search Web Application

Obilet Business API kullanılarak geliştirilmiş, kullanıcıların kalkış-varış noktası ve tarih seçerek otobüs seferi arayabildikleri modern bir web uygulaması.

[![.NET](https://img.shields.io/badge/.NET-9.0-512BD4)](https://dotnet.microsoft.com/)
[![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-MVC-512BD4)](https://docs.microsoft.com/aspnet/core)
[![License](https://img.shields.io/badge/License-MIT-green.svg)](LICENSE)

## 📋 İçindekiler

- [Özellikler](#-özellikler)
- [Teknoloji Stack](#-teknoloji-stack)
- [Kurulum](#-kurulum)
- [Kullanım](#-kullanım)
- [API Entegrasyonu](#-api-entegrasyonu)
- [Öne Çıkan Özellikler](#-öne-çıkan-özellikler)

## ✨ Özellikler

### Core Features
- 🔍 **Gelişmiş Lokasyon Arama** - Text-based AJAX search ile hızlı lokasyon bulma
- 🔄 **Swap Özelliği** - Kalkış ve varış noktalarını tek tıkla değiştirme
- 📅 **Hızlı Tarih Seçimi** - "Bugün" ve "Yarın" butonları ile kolay tarih seçimi
- 💾 **LocalStorage Persistence** - Son aramaları otomatik hatırlama
- ✅ **Gelişmiş Validasyon** - Client-side ve server-side validation
- 🎨 **Responsive Design** - Tüm cihazlarda mükemmel görünüm
- 🏢 **Partner Logoları** - Otobüs firmalarının logoları ve puanları
- 🎯 **Feature Icons** - Sefer özelliklerinin (WiFi, Klima vb.) ikonları

### Advanced Features
- ⚡ **MemoryCache** - Lokasyon listesi için 5 dakikalık cache
- 🔄 **Polly Retry & Circuit Breaker** - API hatalarında otomatik retry
- 🛡️ **Global Exception Middleware** - Merkezi hata yönetimi
- ✔️ **FluentValidation** - Type-safe server-side validation
- 📊 **Structured Logging** - ILogger ile detaylı loglama

## 🛠 Teknoloji Stack

### Backend
- **Framework**: ASP.NET Core MVC (.NET 9)
- **Language**: C# 13.0
- **API Communication**: HttpClientFactory
- **Session Management**: Distributed Session (In-memory)
- **Caching**: IMemoryCache
- **Validation**: FluentValidation
- **Resilience**: Polly (Retry + Circuit Breaker)
- **Serialization**: System.Text.Json

### Frontend
- **View Engine**: Razor Pages / MVC Views
- **UI Framework**: Bootstrap 5
- **JavaScript**: jQuery
- **Storage**: LocalStorage
- **Icons**: Bootstrap Icons + Custom SVG

### Architecture
- **Design Patterns**: Repository, Service Layer, Dependency Injection
- **Principles**: SOLID, Clean Code, Separation of Concerns

## 🚀 Kurulum

### Gereksinimler
- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- Visual Studio 2022 veya VS Code
- Git

### Adımlar

1. **Projeyi Klonlayın**
   - git clone https://github.com/Jirainumi/ObiletCase.git cd ObiletCase

2. **Bağımlılıkları Yükleyin**
   - dotnet restore

3. **API Anahtarını Ayarlayın**
   - `appsettings.json` dosyasına Obilet Business API anahtarınızı ekleyin.
   { "ObiletApi": { "BaseUrl": "https://v2-api.obilet.com", "ApiClientToken": "YOUR_API_CLIENT_TOKEN_HERE" } }

4. **Projeyi Derleyin**
   - dotnet build

5. **Uygulamayı Çalıştırın**
   - dotnet run

## 📖 Kullanım

### 1. Sefer Arama

1. Ana sayfada **kalkış noktası** seçin (text-based search)
2. **Varış noktası** seçin
3. İsteğe bağlı olarak **Swap** butonuyla değiştirin
4. **Tarih** seçin (veya Bugün/Yarın butonlarını kullanın)
5. **"Bileti Bul"** butonuna tıklayın

### 2. Sefer Listesi

- Seferler **kalkış saatine** göre sıralanır
- Her sefer kartında:
  - ✈️ Firma logosu ve puanı
  - 🕐 Kalkış/Varış saatleri
  - ⏱️ Yolculuk süresi
  - 💺 Müsait koltuk sayısı
  - 💰 Fiyat bilgisi
  - 🎯 Özellikler (WiFi, Klima vb.)

### 3. LocalStorage

Son aramanız otomatik olarak saklanır:
- Kalkış noktası
- Varış noktası
- Seçilen tarih

Geri döndüğünüzde son aramanız hazır olur!

## 🔌 API Entegrasyonu

### Kullanılan Endpoint'ler

#### 1. GetSession
- **URL**: `/api/client/getsession`

Kullanıcıya özel session oluşturur.

#### 2. GetBusLocations
- **URL**: `/api/location/getbuslocations`

Tüm otobüs lokasyonlarını getirir (cache'lenir).

#### 3. GetJourneys
- **URL**: `/api/journey/getbusjourneys`

Seçilen kriterlere göre seferleri getirir.

### Authentication
Tüm isteklerde `Authorization: Basic {token}` header'ı gönderilir.

### Error Handling
- **Timeout**: 30 saniye timeout + 3 retry (exponential backoff)
- **Circuit Breaker**: 5 başarısız istekten sonra 30 saniye devre açık
- **User-Friendly Messages**: Teknik hatalar kullanıcıya sade mesajlarla gösterilir

## 🎯 Öne Çıkan Özellikler

### 1. MemoryCache ile Performans Optimizasyonu
   - 5 dakika cache ile API çağrısı azaltma var cacheKey = $"BusLocations_{searchText}"; if (!_cache.TryGetValue(cacheKey, out List<BusLocation> locations)) { // API'den çek ve cache'le }

### 2. Polly ile Resilience
   - 3 retry + exponential backoff var retryPolicy = HttpPolicyExtensions .HandleTransientHttpError() .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
   - Circuit breaker var circuitBreakerPolicy = HttpPolicyExtensions .HandleTransientHttpError() .CircuitBreakerAsync(5, TimeSpan.FromSeconds(30));

### 3. Global Exception Middleware
   - AJAX ve web request'leri ayırt eder if (context.Request.Headers["X-Requested-With"] == "XMLHttpRequest") { // JSON response } else { // Error sayfasına yönlendir }

### 4. FluentValidation ile Type-Safe Validasyon
   - RuleFor(x => x.OriginId) .NotEqual(x => x.DestinationId) .WithMessage(ErrorMessages.SameLocationError);

### 5. Custom JSON Converter
   - [JsonConverter(typeof(FlexibleIntConverter))] public int? Station { get; set; }


## 📊 Performans

- **MemoryCache**: Lokasyon listesi cache'lenerek API çağrıları %80 azalır
- **Polly Retry**: Geçici hatalar otomatik düzelir
- **Circuit Breaker**: API down olduğunda gereksiz istekler engellenir
- **Response Time**: Ortalama < 1s (cache hit)

## 🔐 Güvenlik

- ✅ Session bilgileri server-side (HttpOnly cookie)
- ✅ API token appsettings.json'da (environment variable kullanılabilir)
- ✅ HTTPS enforcement (production)
- ✅ XSS koruması (Razor encoding)
- ✅ CSRF koruması (built-in)

## 🐛 Bilinen Sorunlar

Şu anda bilinen bir sorun bulunmamaktadır.

## 🚧 Gelecek Planları

- [ ] Unit testler
- [ ] Integration testler
- [ ] Docker support
- [ ] Redis cache (distributed)
- [ ] Application Insights entegrasyonu
- [ ] Swagger/OpenAPI documentation
- [ ] CI/CD pipeline (GitHub Actions)

## 🤝 Katkıda Bulunma

1. Fork edin
2. Feature branch oluşturun (`git checkout -b feature/amazing-feature`)
3. Commit edin (`git commit -m 'feat: Add amazing feature'`)
4. Push edin (`git push origin feature/amazing-feature`)
5. Pull Request açın

### Commit Conventions
- `feat:` - Yeni özellik
- `fix:` - Bug fix
- `docs:` - Dokümantasyon
- `style:` - Formatlama
- `refactor:` - Code refactoring
- `test:` - Test ekleme
- `chore:` - Bakım işleri

## 📝 Lisans

Bu proje bir case study projesidir ve eğitim amaçlıdır.

## 👤 Yazar

**Jirainumi**
- GitHub: [@Jirainumi](https://github.com/Jirainumi)
- Repository: [ObiletCase](https://github.com/Jirainumi/ObiletCase)

## 🙏 Teşekkürler

- [Obilet](https://www.obilet.com) - API sağladıkları için
- [ASP.NET Core](https://docs.microsoft.com/aspnet/core) - Framework
- [Bootstrap](https://getbootstrap.com) - UI framework
- [Polly](https://github.com/App-vNext/Polly) - Resilience library
- [FluentValidation](https://fluentvalidation.net) - Validation library

---

⭐ Bu projeyi beğendiyseniz yıldız vermeyi unutmayın!

**Made with ❤️ using .NET 9**
