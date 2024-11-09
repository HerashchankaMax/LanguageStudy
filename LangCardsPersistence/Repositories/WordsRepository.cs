using LangCardsDomain.IRepositories;
using LangCardsDomain.Models;
using MongoDB.Driver;

namespace LangCardsPersistence.Repositories;

public class WordsCardsRepository : IValuableRepository<WordEntity>
{
    private readonly IMongoCollection<WordEntity> _collection;

    public WordsCardsRepository(CardsDbContext dbContext)
    {
        _collection = dbContext.GetAllWords().GetAwaiter().GetResult();
    }

    public async Task<WordEntity> Create(WordEntity flashCard)
    {
        if (flashCard is null)
            throw new ArgumentNullException(nameof(flashCard));
        
         await _collection.InsertOneAsync(flashCard);
         return flashCard;
    }

    public async Task<List<WordEntity>> GetAllAsync()
    {
        return await _collection.Find(card => true).ToListAsync();
    }

    public async Task<WordEntity> GetByIdAsync(Guid guid)
    {
        return await _collection.Find(card => card.Id == guid).FirstOrDefaultAsync();
    }

    public async Task<WordEntity> Update(Guid guid, WordEntity flashCard)
    {
        var existing = await _collection.Find(card => card.Id == guid).FirstOrDefaultAsync();
        if(existing is null)
            throw new ArgumentNullException(nameof(flashCard));
        
        var a = await _collection.ReplaceOneAsync(card => card.Id == guid, flashCard);
        Console.WriteLine($"Replace one result {a.ModifiedCount}");
        return flashCard;
    }

    public async Task Delete(Guid guid)
    {
        await _collection.DeleteOneAsync(card => card.Id == guid);
    }

    public async Task<IEnumerable<WordEntity>> FilterByValue(string searchTerm)
    {
        var result = await _collection.FindAsync(word =>
            string.Compare(word.Value, searchTerm, true) == 0 ||
            string.Compare(word.Definition, searchTerm, true) == 0);
        return result.ToEnumerable();
    }
}