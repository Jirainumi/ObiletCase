using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using ObiletCase.Constants;
using ObiletCase.Models.ViewModels;
using ObiletCase.Services;
using ObiletCase.Validators;

namespace ObiletCase.Controllers;

/// <summary>
/// Sefer arama ve listeleme controller'ı
/// </summary>
public class JourneyController : Controller
{
    private readonly ILogger<JourneyController> _logger;
    private readonly IJourneyService _journeyService;
    private readonly IObiletSessionService _sessionService;
    private readonly IBusLocationService _busLocationService;
    private readonly IValidator<JourneySearchParameters> _validator;

    public JourneyController(
        ILogger<JourneyController> logger,
        IJourneyService journeyService,
        IObiletSessionService sessionService,
        IBusLocationService busLocationService,
        IValidator<JourneySearchParameters> validator)
    {
        _logger = logger;
        _journeyService = journeyService;
        _sessionService = sessionService;
        _busLocationService = busLocationService;
        _validator = validator;
    }

    /// <summary>
    /// Sefer listesi sayfası
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> Index(int originId, int destinationId, DateTime date)
    {
        try
        {
            // FluentValidation ile validate et
            var parameters = new JourneySearchParameters
            {
                OriginId = originId,
                DestinationId = destinationId,
                Date = date
            };

            var validationResult = await _validator.ValidateAsync(parameters);

            if (!validationResult.IsValid)
            {
                TempData["Error"] = validationResult.Errors.First().ErrorMessage;
                return RedirectToAction("Index", "Home");
            }

            // Session al
            var (sessionId, deviceId) = await _sessionService.GetOrCreateSessionAsync(HttpContext);

            if (string.IsNullOrEmpty(sessionId))
            {
                _logger.LogWarning("Session could not be retrieved");
                TempData["Error"] = ErrorMessages.SessionExpired;
                return RedirectToAction("Index", "Home");
            }

            // Seferleri getir
            var journeys = await _journeyService.GetJourneysAsync(
                originId, destinationId, date, sessionId, deviceId);

            // Lokasyon bilgilerini al
            var allLocations = await _busLocationService.GetBusLocationsAsync(
                string.Empty, sessionId, deviceId);

            var originLocation = allLocations.FirstOrDefault(l => l.Id == originId);
            var destinationLocation = allLocations.FirstOrDefault(l => l.Id == destinationId);

            if (originLocation == null || destinationLocation == null)
            {
                _logger.LogWarning("Location not found. Origin: {OriginId}, Dest: {DestId}",
                    originId, destinationId);
                TempData["Error"] = ErrorMessages.LocationNotFound;
                return RedirectToAction("Index", "Home");
            }

            // Sefer bulunamadı kontrolü
            if (!journeys.Any())
            {
                TempData["Warning"] = ErrorMessages.NoJourneysFound;
            }

            var viewModel = new JourneyListViewModel
            {
                Journeys = journeys,
                OriginLocation = originLocation,
                DestinationLocation = destinationLocation,
                JourneyDate = date
            };

            return View(viewModel);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Journey search error. Origin: {OriginId}, Dest: {DestId}, Date: {Date}",
                originId, destinationId, date);
            TempData["Error"] = ErrorMessages.GeneralError;
            return RedirectToAction("Index", "Home");
        }
    }
}