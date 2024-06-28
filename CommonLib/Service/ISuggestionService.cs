using Common.Model;
using CommonLib.Models;

namespace Common.Service
{
    public interface ISuggestionService
    {
        Task<bool> DeleteSuggestionAsync(int id);
        Task<List<Suggestion>> GetAllSuggestionsFromDBAsync();
        Task<bool> InsertSuggestionAsync(SuggestionJson suggestionJson);
        Task<Suggestion> ReadSuggestionAsync(int id);
        Task<bool> UpdateSuggestionAsync(SuggestionJson suggestionJson);
    }
}