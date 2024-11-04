using LangCardsAPI;
using LangCardsApplication;
using LangCardsDomain.IRepositories;
using LangCardsDomain.Models;
using LangCardsPersistence;
using LangCardsPersistence.Repositories;
using WebApplication1;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSingleton<CardsDbContext>();
builder.Services.AddSingleton<IRepository<WordEntity>, WordsCardsRepository>();
builder.Services.AddSingleton<IRepository<FlashCardEntity>, FlashCardsRepository>();
builder.Services.AddSingleton<CardsManipulationManager>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.MapControllers();

app.Run();