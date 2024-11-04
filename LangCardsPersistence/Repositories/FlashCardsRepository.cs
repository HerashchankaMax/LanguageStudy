using LangCardsDomain.IRepositories;
using LangCardsDomain.Models;
using MongoDB.Driver;

namespace LangCardsPersistence.Repositories;

public class FlashCardsRepository :IRepository<FlashCardEntity>
{
    private readonly IMongoCollection<FlashCardEntity> _collection;

    public FlashCardsRepository(CardsDbContext dbContext)
    {
        _collection = dbContext.GetAllCards();
    }

    public async Task<FlashCardEntity> Create(FlashCardEntity flashCard)
    {
        if (flashCard is null)
            throw new ArgumentNullException(nameof(flashCard));
        
         await _collection.InsertOneAsync(flashCard);
         return flashCard;
    }

    public async Task<List<FlashCardEntity>> GetAllAsync()
    {
        return await _collection.Find(card => true).ToListAsync();
    }

    public async Task<FlashCardEntity> GetByIdAsync(Guid guid)
    {
        return await _collection.Find(card => card.Id == guid).FirstOrDefaultAsync();
    }

    public async Task<FlashCardEntity> Update(Guid guid, FlashCardEntity flashCard)
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