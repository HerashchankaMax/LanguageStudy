using LangCardsDomain.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace LangCardsPersistence;

public class CardsDbContext 
{
    private readonly IConfiguration _configuration;
    private readonly IMongoDatabase _mongoDatabase;

    public CardsDbContext(IConfiguration configuration) 
    {
        _configuration = configuration;
        var client = new MongoClient(configuration["ConnectionStrings:CardsConnectionString"]);
        _mongoDatabase = client.GetDatabase(configuration["Cards:DatabaseName"]);
    }

    public IMongoCollection<FlashCardEntity> GetAllCards()
    {
        string? name = _configuration["FlashCards:FalshCardsCollectionName"];
        return _mongoDatabase.GetCollection<FlashCardEntity>(name);
    }
    
    public IMongoCollection<WordEntity> GetAllWords()
    {
        string? name = _configuration["Words:WordsCollectionName"];
        return _mongoDatabase.GetCollection<WordEntity>(name);
    }
}