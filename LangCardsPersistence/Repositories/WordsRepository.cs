using LangCardsDomain.IRepositories;
using LangCardsDomain.Models;
using MongoDB.Driver;

namespace LangCardsPersistence.Repositories;

public class WordsCardsRepository : IRepository<WordEntity>
{
    private readonly IMongoCollection<WordEntity> _collection;

    public WordsCardsRepository(CardsDbContext dbContext)
    {
        _collection = dbContext.GetAllWords();
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
        
        await _collection.ReplaceOneAsync(card => card.Id == guid, flashCard);
        return flashCard;
    }

    public async Task Delete(Guid guid)
    {
        await _collection.DeleteOneAsync(card => card.Id == guid);
    }
}