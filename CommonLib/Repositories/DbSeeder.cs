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
                new Suggestion
                {
                    DateTimeStart = DateTime.Parse("2023-06-01 10:00"),
                    DateTimeEnd = DateTime.Parse("2023-06-01 18:00"),
                    Category = new List<string> { "lunch" },
                    Description = "Ik zou graag Schuddebuikjes willen eten tijdens de lunch. Klik <a href='https://www.ah.nl/producten/ontbijtgranen-broodbeleg-tussendoor/zoet-broodbeleg/hagelslag-vlokken/schuddebuikjes'>hier</a> om hem toe te voegen",
                    Title = "Schuddebuikjes Lunch",
                    UserId = 103,
                    UserName = "Daniël Knippers",
                    EventType = "suggestie"
                },
                new Suggestion
                {
                    DateTimeStart = DateTime.Parse("2023-07-15 08:00"),
                    DateTimeEnd = DateTime.Parse("2023-07-15 17:00"),
                    Category = new List<string> { "fun", "intern", "sportief", "borrel" },
                    Description = "Skiën de vorige keer was echt <i>super</i> awesome. Laten we dat dit jaar weer gaan doen!",
                    Title = "Skiën",
                    UserId = 12,
                    UserName = "Dennis Pullens",
                    EventType = "uitje"
                },
                new Suggestion
                {
                    DateTimeStart = DateTime.Parse("2023-08-20 13:00"),
                    DateTimeEnd = DateTime.Parse("2023-08-20 18:00"),
                    Category = new List<string> { "lunch", "workshop" },
                    Description = "Lunch en workshop over nieuwe trends in digitale marketing.",
                    Title = "Digitale Marketing Workshop",
                    UserId = 45,
                    UserName = "Linda Vermeer",
                    EventType = "suggestie"
                },
                new Suggestion
                {
                    DateTimeStart = DateTime.Parse("2023-09-05 19:00"),
                    DateTimeEnd = DateTime.Parse("2023-09-05 23:00"),
                    Category = new List<string> { "dinner", "networking" },
                    Description = "Gezamenlijk diner en netwerkevent voor nieuwe samenwerkingen.",
                    Title = "Netwerkdiner",
                    UserId = 88,
                    UserName = "Eva Jansen",
                    EventType = "suggestie"
                },
                new Suggestion
                {
                    DateTimeStart = DateTime.Parse("2023-10-12 10:30"),
                    DateTimeEnd = DateTime.Parse("2023-10-12 12:30"),
                    Category = new List<string> { "meeting" },
                    Description = "Belangrijke teammeeting om projectupdates te bespreken.",
                    Title = "Teammeeting",
                    UserId = 21,
                    UserName = "Mark de Vries",
                    EventType = "suggestie"
                },
                new Suggestion
                {
                    DateTimeStart = DateTime.Parse("2023-11-08 14:00"),
                    DateTimeEnd = DateTime.Parse("2023-11-08 17:00"),
                    Category = new List<string> { "team-building", "training" },
                    Description = "Team-building activiteit en trainingssessie voor nieuwe vaardigheden.",
                    Title = "Team-building Training",
                    UserId = 67,
                    UserName = "Sophie Bakker",
                    EventType = "suggestie"
                },
                new Suggestion
                {
                    DateTimeStart = DateTime.Parse("2023-12-03 11:00"),
                    DateTimeEnd = DateTime.Parse("2023-12-03 15:00"),
                    Category = new List<string> { "lunch", "educational" },
                    Description = "Lunch en educatieve sessie over duurzaamheid in het bedrijfsleven.",
                    Title = "Duurzaamheid Lunch",
                    UserId = 39,
                    UserName = "Peter Jacobs",
                    EventType = "suggestie"
                },
                new Suggestion
                {
                    DateTimeStart = DateTime.Parse("2024-01-20 16:00"),
                    DateTimeEnd = DateTime.Parse("2024-01-20 19:00"),
                    Category = new List<string> { "after-work", "social" },
                    Description = "Gezellige borrel na het werk om successen te vieren.",
                    Title = "Successen Borrel",
                    UserId = 55,
                    UserName = "Anne de Boer",
                    EventType = "suggestie"
                },
                new Suggestion
                {
                    DateTimeStart = DateTime.Parse("2024-02-14 09:00"),
                    DateTimeEnd = DateTime.Parse("2024-02-14 12:00"),
                    Category = new List<string> { "training", "workshop" },
                    Description = "Interactieve workshop over effectief leiderschap.",
                    Title = "Leiderschap Workshop",
                    UserId = 78,
                    UserName = "Lisa Peters",
                    EventType = "suggestie"
                },
                new Suggestion
                {
                    DateTimeStart = DateTime.Parse("2024-03-18 10:00"),
                    DateTimeEnd = DateTime.Parse("2024-03-18 16:00"),
                    Category = new List<string> { "conference", "educational" },
                    Description = "Belangrijke conferentie over innovatie in technologie.",
                    Title = "Technologie Conferentie",
                    UserId = 29,
                    UserName = "Tom van Dijk",
                    EventType = "suggestie"
                },
                new Suggestion
                {
                    DateTimeStart = DateTime.Parse("2024-04-22 13:30"),
                    DateTimeEnd = DateTime.Parse("2024-04-22 17:30"),
                    Category = new List<string> { "meeting", "brainstorm" },
                    Description = "Brainstormsessie om nieuwe ideeën voor te stellen en te bespreken.",
                    Title = "Ideeën Brainstorm",
                    UserId = 91,
                    UserName = "Michelle Smit",
                    EventType = "suggestie"
                },
                new Suggestion
                {
                    DateTimeStart = DateTime.Parse("2024-05-10 15:00"),
                    DateTimeEnd = DateTime.Parse("2024-05-10 18:00"),
                    Category = new List<string> { "afternoon", "social" },
                    Description = "Ontspannende middag met collega's om de maand af te sluiten.",
                    Title = "Middag Borrel",
                    UserId = 14,
                    UserName = "Martijn van Leeuwen",
                    EventType = "suggestie"
                },
                new Suggestion
                {
                    DateTimeStart = DateTime.Parse("2024-06-05 08:30"),
                    DateTimeEnd = DateTime.Parse("2024-06-05 10:30"),
                    Category = new List<string> { "breakfast", "networking" },
                    Description = "Gezamenlijk ontbijt en netwerkevent om relaties te versterken.",
                    Title = "Netwerk Ontbijt",
                    UserId = 63,
                    UserName = "Sophie de Wit",
                    EventType = "suggestie"
                },
                new Suggestion
                {
                    DateTimeStart = DateTime.Parse("2024-07-19 12:00"),
                    DateTimeEnd = DateTime.Parse("2024-07-19 15:00"),
                    Category = new List<string> { "lunch", "social" },
                    Description = "Gezellige lunch om teamspirit te versterken.",
                    Title = "Team Lunch",
                    UserId = 36,
                    UserName = "Robert de Jong",
                    EventType = "suggestie"
                },
                new Suggestion
                {
                    DateTimeStart = DateTime.Parse("2024-08-14 09:30"),
                    DateTimeEnd = DateTime.Parse("2024-08-14 11:30"),
                    Category = new List<string> { "morning", "training" },
                    Description = "Ochtendsessie over timemanagement en productiviteit.",
                    Title = "Productiviteit Training",
                    UserId = 82,
                    UserName = "Monique Bakker",
                    EventType = "suggestie"
                },
                new Suggestion
                {
                    DateTimeStart = DateTime.Parse("2024-09-28 14:30"),
                    DateTimeEnd = DateTime.Parse("2024-09-28 18:30"),
                    Category = new List<string> { "workshop", "educational" },
                    Description = "Workshop over strategieën voor bedrijfsgroei.",
                    Title = "Bedrijfsgroei Workshop",
                    UserId = 47,
                    UserName = "Laura Visser",
                    EventType = "suggestie"
                },
                new Suggestion
                {
                    DateTimeStart = DateTime.Parse("2024-10-25 11:00"),
                    DateTimeEnd = DateTime.Parse("2024-10-25 14:00"),
                    Category = new List<string> { "lunch", "meeting" },
                    Description = "Informele lunch en bespreking van projectupdates.",
                    Title = "Project Lunch",
                    UserId = 19,
                    UserName = "Willem van der Linden",
                    EventType = "suggestie"
                },
                new Suggestion
                {
                    DateTimeStart = DateTime.Parse("2024-12-10 13:00"),
                    DateTimeEnd = DateTime.Parse("2024-12-10 17:00"),
                    Category = new List<string> { "workshop", "educational" },
                    Description = "Workshop over nieuwe trends in softwareontwikkeling.",
                    Title = "Softwareontwikkeling Trends",
                    UserId = 104,
                    UserName = "Jasper van Houten",
                    EventType = "suggestie"
                },
                new Suggestion
                {
                    DateTimeStart = DateTime.Parse("2024-12-18 09:00"),
                    DateTimeEnd = DateTime.Parse("2024-12-18 12:00"),
                    Category = new List<string> { "training", "internal" },
                    Description = "Interne training over gebruik van nieuwe tools.",
                    Title = "Tools Training",
                    UserId = 52,
                    UserName = "Marjolein de Graaf",
                    EventType = "suggestie"
                },
                new Suggestion
                {
                    DateTimeStart = DateTime.Parse("2024-12-22 19:00"),
                    DateTimeEnd = DateTime.Parse("2024-12-22 22:00"),
                    Category = new List<string> { "social", "dinner" },
                    Description = "Kerst diner met het hele team om het jaar af te sluiten.",
                    Title = "Kerst Diner",
                    UserId = 37,
                    UserName = "Daan Vermeulen",
                    EventType = "uitje"
                },
                new Suggestion
                {
                    DateTimeStart = DateTime.Parse("2024-12-28 10:00"),
                    DateTimeEnd = DateTime.Parse("2024-12-28 12:00"),
                    Category = new List<string> { "meeting", "brainstorm" },
                    Description = "Brainstormsessie voor het nieuwe jaar plannen.",
                    Title = "Nieuwjaar Brainstorm",
                    UserId = 85,
                    UserName = "Nina van Dam",
                    EventType = "suggestie"
                },
                new Suggestion
                {
                    DateTimeStart = DateTime.Parse("2025-01-05 11:00"),
                    DateTimeEnd = DateTime.Parse("2025-01-05 13:00"),
                    Category = new List<string> { "lunch", "social" },
                    Description = "Nieuwjaarslunch om het jaar goed te beginnen.",
                    Title = "Nieuwjaarslunch",
                    UserId = 60,
                    UserName = "Olaf de Vries",
                    EventType = "suggestie"
                },
                new Suggestion
                {
                    DateTimeStart = DateTime.Parse("2025-01-15 14:00"),
                    DateTimeEnd = DateTime.Parse("2025-01-15 17:00"),
                    Category = new List<string> { "workshop", "creative" },
                    Description = "Creatieve workshop om innovatief denken te stimuleren.",
                    Title = "Creatieve Workshop",
                    UserId = 74,
                    UserName = "Eva Groen",
                    EventType = "suggestie"
                },
                new Suggestion
                {
                    DateTimeStart = DateTime.Parse("2025-01-25 15:00"),
                    DateTimeEnd = DateTime.Parse("2025-01-25 18:00"),
                    Category = new List<string> { "seminar", "educational" },
                    Description = "Seminar over de laatste ontwikkelingen in AI.",
                    Title = "AI Seminar",
                    UserId = 58,
                    UserName = "Bas Mulder",
                    EventType = "suggestie"
                },
                new Suggestion
                {
                    DateTimeStart = DateTime.Parse("2025-02-10 16:00"),
                    DateTimeEnd = DateTime.Parse("2025-02-10 18:00"),
                    Category = new List<string> { "team-building", "fun" },
                    Description = "Team-building activiteit om de teamgeest te versterken.",
                    Title = "Team-building Avond",
                    UserId = 92,
                    UserName = "Laura Bos",
                    EventType = "suggestie"
                },
                new Suggestion
                {
                    DateTimeStart = DateTime.Parse("2025-02-20 10:00"),
                    DateTimeEnd = DateTime.Parse("2025-02-20 13:00"),
                    Category = new List<string> { "meeting", "planning" },
                    Description = "Planning sessie voor Q2 doelen en projecten.",
                    Title = "Q2 Planning",
                    UserId = 48,
                    UserName = "Peter Kuipers",
                    EventType = "suggestie"
                },
                new Suggestion
                {
                    DateTimeStart = DateTime.Parse("2025-02-28 08:00"),
                    DateTimeEnd = DateTime.Parse("2025-02-28 10:00"),
                    Category = new List<string> { "breakfast", "networking" },
                    Description = "Netwerk ontbijt om nieuwe zakelijke relaties op te bouwen.",
                    Title = "Zakelijk Ontbijt",
                    UserId = 33,
                    UserName = "Sandra van der Meer",
                    EventType = "suggestie"
                }
            };

            _context.Suggestions.AddRange(suggestions);
            _context.SaveChanges();
        }
    }
}