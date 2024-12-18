using LangCardsDomain.IRepositories;
using LangCardsDomain.Models;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace LangCardsPersistence.Repositories;

public class WordsRepository : IValuableRepository<WordEntity>
{
    private readonly IMongoCollection<WordEntity> _collection;
    private readonly ILogger<WordsRepository> _logger;

    public WordsRepository(CardsDbContext dbContext, ILogger<WordsRepository> logger)
    {
        _logger = logger;
        _collection = dbContext.GetAllWords().GetAwaiter().GetResult();
    }

    public async Task<List<WordEntity>> GetAllAsync()
    {
        return await _collection.Find(card => true).ToListAsync();
    }

    public async Task<WordEntity> GetByIdAsync(Guid guid)
    {
        var result = await _collection.Find(card => card.Id == guid).FirstOrDefaultAsync();
        if (result != null)
            _logger.LogInformation($"Found word: {result.Id}");
        else
            _logger.LogWarning($"Word with id {guid} is not found");
        return result;
    }

    public async Task Delete(Guid guid)
    {
        var result = await _collection.DeleteOneAsync(card => card.Id == guid);
        if (result.DeletedCount == 0) _logger.LogWarning($"Deletion of word with guid {guid} was not successful");
        _logger.LogInformation($"Deleted word with id {guid}");
    }

    public async Task<IEnumerable<WordEntity>> FilterByValue(string searchTerm)
    {
        var result = await _collection.FindAsync(word =>
            string.Compare(word.Value, searchTerm, true) == 0 ||
            string.Compare(word.Definition, searchTerm, true) == 0);
        var filterByValue = result.ToEnumerable();
        _logger.LogInformation($"There are {filterByValue.Count()} with '{searchTerm}' in their content");
        return filterByValue;
    }

    public async Task<WordEntity> Create(WordEntity flashCard)
    {
        if (flashCard is null)
            throw new ArgumentNullException(nameof(flashCard));
        try
        {
            await _collection.InsertOneAsync(flashCard);
            return flashCard;
        }
        catch (Exception e)
        {
            _logger.LogError($"Failed to create word: {e}");
            return null;
        }
    }

    public async Task<WordEntity> Update(Guid guid, WordEntity flashCard)
    {
        var existing = await _collection.Find(card => card.Id == guid).FirstOrDefaultAsync();
        if (existing is null)
            _logger.LogError($"Updating isn't possible, word with id: {guid} is not found");

        var replacingResult = await _collection.ReplaceOneAsync(card => card.Id == guid, flashCard);
        _logger.LogInformation($"Number of replaced word with id {guid} is {replacingResult.ModifiedCount}");
        return await GetByIdAsync(flashCard.Id);
    }
}