using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ObiletCase.Constants;
using ObiletCase.Models;
using ObiletCase.Models.ResponseModels;
using ObiletCase.Models.ViewModels;
using ObiletCase.Services;

namespace ObiletCase.Controllers;

/// <summary>
/// Ana sayfa controller'ı
/// </summary>
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IBusLocationService _busLocationService;
    private readonly IObiletSessionService _sessionService;

    public HomeController(
        ILogger<HomeController> logger,
        IBusLocationService busLocationService,
        IObiletSessionService sessionService)
    {
        _logger = logger;
        _busLocationService = busLocationService;
        _sessionService = sessionService;
    }

    /// <summary>
    /// Ana arama sayfası
    /// </summary>
    public async Task<IActionResult> Index()
    {
        try
        {
            // Session oluştur
            var (sessionId, deviceId) = await _sessionService.GetOrCreateSessionAsync(HttpContext);

            if (string.IsNullOrEmpty(sessionId))
            {
                _logger.LogWarning("Session could not be created");
                TempData["Error"] = ErrorMessages.SessionCreateError;
            }

            // Tüm lokasyonları getir
            var locations = await _busLocationService.GetBusLocationsAsync(string.Empty, sessionId, deviceId);

            if (!locations.Any())
            {
                _logger.LogWarning("No locations loaded");
                TempData["Error"] = ErrorMessages.LocationsLoadError;
            }

            var viewModel = new SearchViewModel
            {
                Locations = locations,
                SelectedDate = DateTime.Now.AddDays(1) // Yarın
            };

            return View(viewModel);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Home page load error");
            TempData["Error"] = ErrorMessages.GeneralError;
            return View(new SearchViewModel { Locations = new List<BusLocation>() });
        }
    }

    /// <summary>
    /// Lokasyon arama API endpoint'i (AJAX için)
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> SearchLocations(string searchText)
    {
        try
        {
            var (sessionId, deviceId) = await _sessionService.GetOrCreateSessionAsync(HttpContext);

            if (string.IsNullOrEmpty(sessionId))
            {
                return Json(new { error = true, message = ErrorMessages.SessionExpired });
            }

            var locations = await _busLocationService.GetBusLocationsAsync(searchText, sessionId, deviceId);

            return Json(locations);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Location search error");
            return Json(new { error = true, message = ErrorMessages.LocationsLoadError });
        }
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}