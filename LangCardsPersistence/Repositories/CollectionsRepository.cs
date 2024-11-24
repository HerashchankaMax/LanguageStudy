using LangCardsDomain.IRepositories;
using LangCardsDomain.Models;
using LangCardsPersistence.Request;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace LangCardsPersistence.Repositories;

public class CollectionsRepository : IValuableRepository<CollectionEntity>
{
    private readonly CardsDbContext _cardsDbContext;
    private readonly IMongoCollection<CollectionEntity> _collections;
    private readonly ILogger<CollectionsRepository> _logger;

    public CollectionsRepository(CardsDbContext cardsDbContext, ILogger<CollectionsRepository> logger)
    {
        _cardsDbContext = cardsDbContext;
        _logger = logger;
        _collections = cardsDbContext.GetAllCollections().GetAwaiter().GetResult();
    }

    public async Task<List<CollectionEntity>> GetAllAsync()
    {
        return await _collections.Find(x => x.Id != null).ToListAsync();
    }

    public async Task<CollectionEntity> GetByIdAsync(Guid guid)
    {
        return await _collections.Find(x => x.Id == guid).FirstOrDefaultAsync();
    }

    public async Task Delete(Guid guid)
    {
        await _collections.DeleteOneAsync(x => x.Id == guid);
    }

    public async Task<IEnumerable<CollectionEntity>> FilterByValue(string searchTerm)
    {
        var result = await _collections.FindAsync(x =>
            x.Description.Contains(searchTerm)
            || x.Name.Contains(searchTerm));
        var filterByValue = result.ToEnumerable();
        return filterByValue;
    }

    public async Task<CollectionEntity> AddWord(Guid collectionId, WordEntity word)
    {
        var filer = Builders<CollectionEntity>.Filter.Eq(x => x.Id, collectionId);
        var update = Builders<CollectionEntity>.Update.AddToSet(x => x.WordGuids, word.Id);
        var options = new FindOneAndUpdateOptions<CollectionEntity>
        {
            ReturnDocument = ReturnDocument.After
        };
        var res = await _collections.FindOneAndUpdateAsync(filer, update, options);
        return res;
    }

    public async Task<CollectionEntity> Create(CollectionEntity item)
    {
        await _collections.InsertOneAsync(item);
        return item;
    }

    public async Task<CollectionEntity> Update(Guid guid, UpdateCollectionRequest updatingInfo)
    {
        var filter = Builders<CollectionEntity>.Filter.Eq(x => x.Id, guid);
        var update = Builders<CollectionEntity>.Update
            .Set(x => x.Name, updatingInfo.Description)
            .Set(x => x.WordGuids, updatingInfo.Words.Select(x => x.Id))
            .Set(x => x.Description, updatingInfo.Description);
        var result = await _collections.UpdateOneAsync(filter, update);
        return await _collections.Find(x => x.Id == guid).FirstOrDefaultAsync();
    }
}