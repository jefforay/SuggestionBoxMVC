using Common.Model;
using Common.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR.Client;
using SuggestionBoxMVC.Models;
using System.Diagnostics;

namespace SuggestionAPI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HubConnection _hubConnection;
        private List<Suggestion> _suggestions = new();
        private readonly ISuggestionService _suggestionService;

        public HomeController(ILogger<HomeController> logger, ISuggestionService suggestionService)
        {
            _logger = logger;
            _suggestionService = suggestionService;
            _suggestions = _suggestionService.GetAllSuggestionsFromDBAsync().GetAwaiter().GetResult();
            _hubConnection = new HubConnectionBuilder()
                .WithUrl("http://localhost/loghub")
                .WithAutomaticReconnect()
                .Build();

            SetHub();
        }

        public IActionResult Index(string eventType)
        {
            try
            {
                var orderedSuggestions = _suggestions.OrderByDescending(x => x.Id).ToList();

                if (!orderedSuggestions.Any())
                {
                    return View();
                }

                if (!string.IsNullOrEmpty(eventType))
                {
                    orderedSuggestions = orderedSuggestions.Where(x => x.EventType == eventType).ToList();
                }

                return View(orderedSuggestions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while processing Index action.");
                throw;
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

        private void SetHub()
        {
            _hubConnection.On<Suggestion>("receiveSuggestion", suggestion =>
            {
                var existingSuggestion = _suggestions.FirstOrDefault(s => s.Id == suggestion.Id);
                if (existingSuggestion == null)
                {
                    _suggestions.Insert(0, suggestion);
                }
            });

            _hubConnection.StartAsync().ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    Console.WriteLine($"Error starting connection: {task.Exception?.Message}");
                }
            });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _hubConnection.DisposeAsync();
            }
            base.Dispose(disposing);
        }
    }
}
