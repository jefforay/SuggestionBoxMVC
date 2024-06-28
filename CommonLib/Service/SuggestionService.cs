using Microsoft.AspNetCore.SignalR;
using Common.Model;
using Common.Repositories;
using Common.Hubs;
using CommonLib.Models;

namespace Common.Service;

public sealed class SuggestionService(IHubContext<SuggestionHub> suggestionHub, ISuggestionRepository suggestionRepository) : ISuggestionService
{
    private readonly IHubContext<SuggestionHub> _suggestionHub = suggestionHub;
    private readonly ISuggestionRepository _suggestionRepository = suggestionRepository;

    public Task<Suggestion> ReadSuggestionAsync(int id)
        => _suggestionRepository.ReadSuggestionAsync(id);

    public Task<List<Suggestion>> GetAllSuggestionsFromDBAsync()
        => _suggestionRepository.GetAllSuggestionsFromDBAsync();

    public async Task<bool> InsertSuggestionAsync(SuggestionJson suggestionJson)
    {
        Suggestion suggestion = ConvertToSuggestion(suggestionJson);
        await _suggestionRepository.InsertSuggestionAsync(suggestion);
        await _suggestionHub.Clients.All.SendAsync("receiveSuggestion", suggestion);

        return true;
    }

    public async Task<bool> UpdateSuggestionAsync(SuggestionJson suggestionJson)
    {
        Suggestion suggestion = ConvertToSuggestion(suggestionJson);
        await _suggestionRepository.UpdateSuggestionAsync(suggestion);
        await _suggestionHub.Clients.All.SendAsync("updateSuggestion", suggestion);

        return true;
    }

    public async Task<bool> DeleteSuggestionAsync(int id)
    {
        await _suggestionRepository.DeleteSuggestionAsync(id);
        await _suggestionHub.Clients.All.SendAsync("removeSuggestion", id);

        return true;
    }

    private Suggestion ConvertToSuggestion(SuggestionJson dto)
    {
        DateTime? start = DateTime.Now;
        DateTime? end = DateTime.Now;

        if (dto.Type == "uitje")
        {
            if (DateTime.TryParse(dto.BeginDatum, out DateTime startDate))
            {
                start = startDate;
            }

            if (DateTime.TryParse(dto.EindDatum, out DateTime endDate))
            {
                end = endDate;
            }
        }

        List<string> categories = dto.Categories != null ? new List<string>(dto.Categories) : new List<string>();

        return new Suggestion
        {
            Id = dto.Id,
            Title = dto.Onderwerp,
            Description = dto.Beschrijving,
            UserId = dto.UserId,
            UserName = dto.UserName,
            EventType = dto.Type,
            DateTimeStart = start,
            DateTimeEnd = end,
            Category = categories
        };
    }
}
