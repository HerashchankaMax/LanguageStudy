using LangCardsAPI;
using LangCardsApplication;
using LangCardsDomain.IRepositories;
using LangCardsDomain.Models;
using LangCardsPersistence;
using LangCardsPersistence.Repositories;
using LangWordsApplication;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddSingleton<CardsDbContext>();
builder.Services.AddSingleton<IValuableRepository<WordEntity>, WordsCardsRepository>();
builder.Services.AddSingleton<IRepository<FlashCardEntity>, FlashCardsRepository>();
builder.Services.AddTransient<WordsManipulationManager>();
builder.Services.AddTransient<CardsManipulationManager>();
BsonSerializer.RegisterSerializer(typeof(Guid), new GuidSerializer(GuidRepresentation.Standard));

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

app.Run();