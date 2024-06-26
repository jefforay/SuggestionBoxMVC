using Common.Model;

namespace Common.Service
{
    public interface ISuggestionService
    {
        Task<bool> DeleteSuggestionAsync(int id);
        Task<List<Suggestion>> GetAllSuggestionsFromDBAsync();
        Task<bool> InsertSuggestionAsync(Suggestion suggestion);
        Task<Suggestion> ReadSuggestionAsync(int id);
        Task<bool> UpdateSuggestionAsync(Suggestion suggestion);
    }
}