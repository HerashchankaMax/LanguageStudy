using LangCardsDomain.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;

namespace LangCardsPersistence;

public class CardsDbContext 
{
    private readonly IConfiguration _configuration;
    private readonly IMongoDatabase _mongoDatabase;

    public CardsDbContext(IConfiguration configuration) 
    {
        _configuration = configuration;
        var rawConnetionString = configuration["ConnectionStrings:CardsConnectionString"];
        var dbUser = configuration["dbUser"];
        var dbPassword = configuration["dbPassword"];
        rawConnetionString = rawConnetionString.Replace("<db_user>", dbUser);
        rawConnetionString = rawConnetionString.Replace("<db_password>", dbPassword);
        
        var client = new MongoClient(rawConnetionString);
        _mongoDatabase = client.GetDatabase(configuration["FlashCards:DatabaseName"]);
        var wordsColletction = _mongoDatabase.GetCollection<WordEntity>(configuration["FlashCards:WordsCollectionName"]);
        
        if(wordsColletction.CountDocuments(new BsonDocument() )== 0)
        {
            var seedWord = new WordEntity("Hello", "Greeting", "Привет");
            wordsColletction.InsertOne(seedWord);
        }
        var cardsCollection = _mongoDatabase.GetCollection<FlashCardEntity>(configuration["FlashCards:FlashCardsCollectionName"]);
        
        if(cardsCollection.CountDocuments(new BsonDocument() )== 0)
        {
            WordEntity? word = wordsColletction.Find
                (x=>x.Id.ToString() == "4f79e34a-c9be-46d0-9b41-6696f0efd461")
                .FirstOrDefault();
            var seedCard = new FlashCardEntity(word);
            cardsCollection.InsertOne(seedCard);
        }
    }

    public IMongoCollection<FlashCardEntity> GetAllCards()
    {
        string? name = _configuration["FlashCards:FlashCardsCollectionName"];
        return _mongoDatabase.GetCollection<FlashCardEntity>(name);
    }
    
    public async Task<IMongoCollection<WordEntity>> GetAllWords()
    {
        string? name = _configuration["FlashCards:WordsCollectionName"];
        var mongoCollection = _mongoDatabase.GetCollection<WordEntity>(name);
        return mongoCollection;
    }
}