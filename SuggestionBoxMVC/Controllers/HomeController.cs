using Common.Model;
using Common.Service;
using CommonLib.Models;
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


        [HttpGet("Home/{id:int}")]
        //[ResponseCache(Duration = 60 * 60 * 24, Location = ResponseCacheLocation.Any, NoStore = false)]
        public async Task<ActionResult<Suggestion>> Get(int id)
        {
            try
            {
                return Ok(await _suggestionService.ReadSuggestionAsync(id));
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "Failed reading a suggestion through the controller");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Home")]
        //[ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any, NoStore = false)]
        public async Task<ActionResult<List<Suggestion>>> Get()
        {
            try
            {
                return Ok(await Task.Run(_suggestionService.GetAllSuggestionsFromDBAsync));
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "Failed reading a suggestion list through the controller");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Home")]
        public async Task<IActionResult> Post([FromBody] SuggestionJson suggestionJson)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                return Ok(await _suggestionService.InsertSuggestionAsync(suggestionJson));
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "Failed inserting a suggestion through the controller");
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("Home/Edit")]
        public async Task<IActionResult> Put([FromBody] SuggestionJson suggestionJson)
        {
            try
            {
                return Ok(await _suggestionService.UpdateSuggestionAsync(suggestionJson));
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "Failed updating a suggestion through the controller");
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("Home/Delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _suggestionService.DeleteSuggestionAsync(id);
                if (result)
                {
                    return Ok(new { success = true });
                }
                return NotFound(new { success = false, message = "Suggestion not found" });
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "Failed deleting a suggestion through the controller");
                return BadRequest(new { success = false, message = ex.Message });
            }
        }
    }
}
