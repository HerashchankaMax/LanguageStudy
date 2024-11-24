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
        var wordsCollection =
            _mongoDatabase.GetCollection<WordEntity>(configuration["FlashCards:WordsCollectionName"]);

        if (wordsCollection.CountDocuments(new BsonDocument()) == 0)
        {
            var seedWord = new WordEntity("Hello", "Greeting", "Привет");
            wordsCollection.InsertOne(seedWord);
        }


        var newCollection = _mongoDatabase.GetCollection<CollectionEntity>(configuration["FlashCards:CollectionsName"]);
        var words = wordsCollection.Find(new BsonDocument()).ToList();

        if (newCollection.CountDocuments(new BsonDocument()) == 0)
        {
            var seedData = new CollectionEntity("Test collection", words);
            newCollection.InsertOne(seedData);
        }
    }


    public async Task<IMongoCollection<WordEntity>> GetAllWords()
    {
        var name = _configuration["FlashCards:WordsCollectionName"];
        var mongoCollection = _mongoDatabase.GetCollection<WordEntity>(name);
        return mongoCollection;
    }

    public async Task<IMongoCollection<CollectionEntity>> GetAllCollections()
    {
        var name = _configuration["FlashCards:CollectionsName"];
        var mongoCollection = _mongoDatabase.GetCollection<CollectionEntity>(name);
        return mongoCollection;
    }
}