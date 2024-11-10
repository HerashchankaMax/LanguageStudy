using LangCardsAPI;
using LangCardsApplication;
using LangCardsDomain.IRepositories;
using LangCardsDomain.Models;
using LangCardsPersistence;
using LangCardsPersistence.Repositories;
using LangWordsApplication;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;using Serilog;
using Serilog.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddSingleton<CardsDbContext>();
builder.Services.AddSingleton<IValuableRepository<WordEntity>, WordsCardsRepository>();
builder.Services.AddSingleton<IRepository<FlashCardEntity>, FlashCardsRepository>();
builder.Services.AddTransient<WordsManipulationManager>();
builder.Services.AddTransient<CardsManipulationManager>();
BsonSerializer.RegisterSerializer(typeof(Guid), new GuidSerializer(GuidRepresentation.Standard));

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .Enrich.FromLogContext()
    .Enrich.WithProperty("Application", "LangCardsAPI")
    .WriteTo.Console()
    .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();
builder.Host.UseSerilog();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.MapControllers();
app.Use(async (context, next) =>
{
    var correlationId = Guid.NewGuid().ToString();
    LogContext.PushProperty("CorrelationId", correlationId);  // Adds CorrelationId to each log in this request
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


app.Run();