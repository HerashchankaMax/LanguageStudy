using LangCardsApplication.Requests;
using LangCardsDomain.IRepositories;
using LangCardsDomain.Models;
using LangCardsPersistence.Repositories;
using LangCardsPersistence.Request;
using LangWordsApplication;
using Microsoft.Extensions.Logging;

namespace LangCardsApplication;

public class CollectionsManipulationManager
{
    private readonly CollectionsRepository _collectionsRepository;
    private readonly ILogger<CollectionsManipulationManager> _logger;
    private readonly WordsManipulationManager _wordsManipulationManager;

    public CollectionsManipulationManager(
        IValuableRepository<CollectionEntity> collectionsRepository,
        WordsManipulationManager wordsManipulationManager,
        ILogger<CollectionsManipulationManager> logger
    )
    {
        _collectionsRepository = collectionsRepository as CollectionsRepository;
        _wordsManipulationManager = wordsManipulationManager;
        _logger = logger;
    }

    public async Task<IEnumerable<CollectionEntity>> GetCollectionsByFilter(string text)
    {
        var collections = await _collectionsRepository.FilterByValue(text);
        _logger.LogInformation($"Found {collections.Count()} collections with '{text}' in their content");
        return collections;
    }

    public async Task<IEnumerable<CollectionEntity>> GetCollections()
    {
        return await _collectionsRepository.GetAllAsync();
    }

    public async Task<CollectionEntity?> GetCollectionByIdAsync(Guid cardId)
    {
        return await _collectionsRepository.GetByIdAsync(cardId);
    }

    public async Task<CollectionEntity?> CreateCollection(CollectionDataCommandRequest commandRequest)
    {
        var createdCards = new List<WordEntity>();
        foreach (var card in commandRequest.Words)
        {
            var res = await _wordsManipulationManager.CreateWordAsync(card);
            createdCards.Add(res);
        }

        CollectionEntity newCollection = new(commandRequest.CollectionName, createdCards);
        newCollection.Description = commandRequest.Description;
        _logger.LogInformation(
            $"Created collection with id {newCollection.Id} - with {createdCards.Count} words");
        return await _collectionsRepository.Create(newCollection);
    }

    public async Task<CollectionEntity?> UpdateCollectionAsync(
        CollectionDataCommandRequest collectionData, Guid collectionId)
    {
        var updatingCollection = await _collectionsRepository.GetByIdAsync(collectionId);
        if (updatingCollection == null)
            _logger.LogError($"Collection updating failed. Collection with id {collectionId} not found");

        var updatedWords = new List<WordEntity>();
        collectionData.Words.ForEach(
            async w =>
            {
                var word = await _wordsManipulationManager.CreateWordAsync(w);
                updatedWords.Add(word);
            });
        var updateData =
            new UpdateCollectionRequest(updatedWords, collectionData.CollectionName, collectionData.Description);
        await _collectionsRepository.Update(collectionId, updateData);
        return await _collectionsRepository.GetByIdAsync(collectionId);
    }

    public async Task DeleteCollection(Guid cardId)
    {
        await _collectionsRepository.Delete(cardId);
    }

    public async Task<bool> AddWord(Guid collectionId, CreateWordCommandRequest request)
    {
        var initialState = (await _collectionsRepository.GetByIdAsync(collectionId)).WordGuids.ToList().Count();
        var word = await _wordsManipulationManager.CreateWordAsync(request);
        var updatedCollection = await _collectionsRepository.AddWord(collectionId, word);
        var updatedState = updatedCollection.WordGuids.ToList().Count();
        var wordAdded = updatedState > initialState;
        if (wordAdded)
            _logger.LogInformation($"Collection ({collectionId}) has been updated. {request.Word} has been added.");
        else
            _logger.LogError($"Collection ({collectionId}) hasn't been updated. {request.Word} hasn't been added.");

        return wordAdded;
    }
}