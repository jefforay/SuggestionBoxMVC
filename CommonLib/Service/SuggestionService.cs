using Microsoft.AspNetCore.SignalR;
using Common.Model;
using Common.Repositories;
using Common.Hubs;

namespace Common.Service;

public sealed class SuggestionService(IHubContext<SuggestionHub> suggestionHub, ISuggestionRepository suggestionRepository) : ISuggestionService
{
    private readonly IHubContext<SuggestionHub> _suggestionHub = suggestionHub;
    private readonly ISuggestionRepository _suggestionRepository = suggestionRepository;

    public Task<Suggestion> ReadSuggestionAsync(int id)
        => _suggestionRepository.ReadSuggestionAsync(id);

    public Task<List<Suggestion>> GetAllSuggestionsFromDBAsync()
        => _suggestionRepository.GetAllSuggestionsFromDBAsync();

    public async Task<bool> InsertSuggestionAsync(Suggestion suggestion)
    {
        await _suggestionRepository.InsertSuggestionAsync(suggestion);
        await _suggestionHub.Clients.All.SendAsync("receiveSuggestion", suggestion);

        return true;
    }

    public async Task<bool> UpdateSuggestionAsync(Suggestion suggestion)
    {
        await _suggestionRepository.UpdateSuggestionAsync(suggestion);
        await _suggestionHub.Clients.All.SendAsync("editSuggestion", suggestion);

        return true;
    }

    public async Task<bool> DeleteSuggestionAsync(int id)
    {
        await _suggestionRepository.DeleteSuggestionAsync(id);
        await _suggestionHub.Clients.All.SendAsync("removeSuggestion", id);

        return true;
    }
}
