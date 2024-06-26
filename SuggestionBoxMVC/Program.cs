using Microsoft.EntityFrameworkCore;
using Common.Repositories;
using Common.Options;
using Common.Service;
using Common.Hubs;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();
builder.Services.AddScoped<ISuggestionRepository, SuggestionRepository>();
builder.Services.AddScoped<ISuggestionService, SuggestionService>();

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDbContext<SuggestionDbContext>(options =>
            options.UseInMemoryDatabase("SuggestionBox"));
    builder.Services.AddTransient<DbSeeder>();
    var serviceProvider = builder.Services.BuildServiceProvider();
    var seeder = serviceProvider.GetRequiredService<DbSeeder>();
    seeder.SeedData();
}
else
{
    builder.Services.Configure<ConnectionSqlOptions>(builder.Configuration.GetSection("ConnectionSqlOptions"));
    var connectionOptions = builder.Configuration.GetSection("ConnectionSqlOptions").Get<ConnectionSqlOptions>();
    builder.Services.AddDbContext<SuggestionDbContext>(options =>
        options.UseSqlServer(connectionOptions.DefaultConnection, sqlServerOptionsAction: sqlOptions =>
        {
            sqlOptions.EnableRetryOnFailure(
                maxRetryCount: 5,
                maxRetryDelay: TimeSpan.FromSeconds(30),
                errorNumbersToAdd: null);
        }));
}

builder.Services.AddControllersWithViews();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseResponseCaching();
app.UseAuthorization();

app.MapControllers();
app.MapHub<SuggestionHub>("/suggestionhub").AllowAnonymous();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();