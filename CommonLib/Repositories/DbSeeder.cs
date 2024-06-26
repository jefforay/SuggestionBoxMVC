using Common.Model;
using Common.Repositories;

public class DbSeeder
{
    private readonly SuggestionDbContext _context;

    public DbSeeder(SuggestionDbContext context)
    {
        _context = context;
    }

    public void SeedData()
    {
        if (!_context.Suggestions.Any())
        {
            var suggestions = new List<Suggestion>
            {
                new Suggestion { DateTimeStart = DateTime.Now, DateTimeEnd = DateTime.Now.AddDays(1), Category = "Category 1", Description = "Description 1", Title = "Title 1", UserId = 1, UserName = "User 1", EventType = "Event Type 1" },
                new Suggestion { DateTimeStart = DateTime.Now, DateTimeEnd = DateTime.Now.AddDays(2), Category = "Category 2", Description = "Description 2", Title = "Title 2", UserId = 2, UserName = "User 2", EventType = "Event Type 2" },
            };

            _context.Suggestions.AddRange(suggestions);
            _context.SaveChanges();
        }
    }
}