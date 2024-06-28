using Common.Model;
using Microsoft.EntityFrameworkCore;

namespace Common.Repositories;

public sealed class SuggestionRepository(SuggestionDbContext context) : ISuggestionRepository
{
    private readonly DbSet<Suggestion> _suggestionTable = context.Set<Suggestion>();
    private readonly SuggestionDbContext _context = context;

    public async Task<bool> DeleteSuggestionAsync(int id)
    {
        Suggestion? suggestion = await _suggestionTable.FindAsync(id);

        if (suggestion != null)
        {
            _suggestionTable.Remove(suggestion);
            await _context.SaveChangesAsync();
        }

        return true;
    }

    public async Task<bool> InsertSuggestionAsync(Suggestion suggestion)
    {
        await _suggestionTable.AddAsync(suggestion);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> UpdateSuggestionAsync(Suggestion suggestion)
    {
        Suggestion? existingSuggestion = await _suggestionTable.FindAsync(suggestion.Id);

        if (existingSuggestion != null)
        {
            existingSuggestion.DateTimeStart = suggestion.DateTimeStart;
            existingSuggestion.DateTimeEnd = suggestion.DateTimeEnd;
            existingSuggestion.Category = suggestion.Category;
            existingSuggestion.Description = suggestion.Description;
            existingSuggestion.Title = suggestion.Title;
            existingSuggestion.UserId = suggestion.UserId;
            existingSuggestion.UserName = suggestion.UserName;
            existingSuggestion.EventType = suggestion.EventType;

            await _context.SaveChangesAsync();
            return true;
        }

        return false;
    }

    public async Task<Suggestion> ReadSuggestionAsync(int id)
        => await _suggestionTable.FirstOrDefaultAsync(suggestion => suggestion.Id == id) ?? new Suggestion();

    public async Task<List<Suggestion>> GetAllSuggestionsFromDBAsync()
    => await _suggestionTable
            .OrderByDescending(suggestion => suggestion.Id)
            .ToListAsync();
}

