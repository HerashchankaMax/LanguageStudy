using LangCardsAPI.Services;
using LangCardsApplication;
using LangCardsDomain.Models;
using LangCardsPersistence;
using LangWordsApplication;
using Microsoft.AspNetCore.Identity;
using Serilog;
using Serilog.Context;

var builder = WebApplication.CreateBuilder(args);
builder.ConfigureLogger();

builder.Services.ConfigureControllers(builder.Configuration);
builder.Services.AddScoped<TokenService>();
builder.Services.AddCardDataBase(builder.Configuration);
builder.Services.AddTransient<WordsManipulationManager>();
builder.Services.AddTransient<CollectionsManipulationManager>();
builder.Services.AddIdentityContext(builder.Configuration);
builder.Services.ConfigureIdentity();
builder.Services.ConfigureAuthentication(builder.Configuration);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.UseCors(builder => { builder.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:3000"); });
;
;
app.Use(async (context, next) =>
{
    var correlationId = Guid.NewGuid().ToString();
    LogContext.PushProperty("CorrelationId", correlationId); // Adds CorrelationId to each log in this request
    await next.Invoke();
});

app.Use(async (context, next) =>
{
    if (context.User.Identity?.IsAuthenticated == true)
    {
        var userId = context.User.FindFirst("sub")?.Value ?? "unknown";
        LogContext.PushProperty("UserId", userId);
    }

    await next.Invoke();
});

try
{
    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<IdentityContext>();
    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
    await DataSeedService.SeedData(context, userManager);
}
catch (Exception e)
{
    Log.Error(e, "An error occurred while seeding the database.");
}

app.Run();