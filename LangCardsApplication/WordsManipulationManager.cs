using LangCardsApplication.Requests;
using LangCardsDomain.IRepositories;
using LangCardsDomain.Models;
using Microsoft.Extensions.Logging;

namespace LangWordsApplication;

public class WordsManipulationManager
{
    private readonly IRepository<WordEntity> _wordRepository;
    private readonly ILogger<WordsManipulationManager> _logger;

    public WordsManipulationManager( IValuableRepository<WordEntity> wordRepository, ILogger<WordsManipulationManager> logger )
    {
        _wordRepository = wordRepository;
        _logger = logger;
    }

    public async Task<IEnumerable<WordEntity>> GetWordsAsync()
    {
        var words = await _wordRepository.GetAllAsync();
        _logger.LogInformation( $"{words.Count()} have been retrieved words from database");
        return words;
    }

    public async Task<WordEntity?> GetWordByIdAsync(Guid wordId)
    {
        var result = await _wordRepository.GetByIdAsync(wordId);
        if (result == null)
        {
            _logger.LogInformation($"Word with id - {wordId} is not found in database");
        }
        return result;
    }

    public async Task<WordEntity?> CreateWordAsync(CreateWordCommandRequest commandRequest)
    {
        var newWord = new WordEntity(commandRequest.Word, commandRequest.Definition, commandRequest.Translation);
        return await _wordRepository.Create(newWord);
    }
    public async Task<WordEntity?> UpdateWordAsync(CreateWordCommandRequest commandRequest, Guid wordId)
    {
        var updatingWord = await _wordRepository.GetByIdAsync(wordId);
        if (updatingWord == null)
        {
            _logger.LogInformation($"Updating Failed. Word with id - {wordId} is not found in database");
            return null;
        }
        
        updatingWord.UpdateDefinition( commandRequest.Definition);
        updatingWord.UpdateValue(commandRequest.Word);
       
        return await _wordRepository.Update(wordId, updatingWord);
    }

    public async Task DeleteWordAsync(Guid wordId)
    {
        try
        {
            await _wordRepository.Delete(wordId);
        }
        catch (Exception e)
        {
            _logger.LogInformation($"Deletion failed. {e.Message}");
        }
    }
}