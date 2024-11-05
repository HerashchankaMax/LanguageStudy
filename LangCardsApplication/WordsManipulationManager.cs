using LangCardsApplication.Requests;
using LangCardsDomain.IRepositories;
using LangCardsDomain.Models;

namespace LangWordsApplication;

public class WordsManipulationManager
{
    private readonly IRepository<WordEntity> _wordRepository;

    public WordsManipulationManager( IRepository<WordEntity> wordRepository)
    {
        _wordRepository = wordRepository;
    }

    public async Task<IEnumerable<WordEntity>> GetWordsAsync()
    {
        var words = await _wordRepository.GetAllAsync();
        return words;
    }

    public async Task<WordEntity?> GetWordByIdAsync(Guid wordId)
    {
        return await _wordRepository.GetByIdAsync(wordId);
    }

    public async Task<WordEntity?> CreateWordAsync(CreateWordCommandRequest commandRequest)
    {
        var newWord = new WordEntity(commandRequest.Word, commandRequest.Definition, commandRequest.Translation);
        return await _wordRepository.Create(newWord);
    }
    public async Task<WordEntity?> UpdateWordAsync(CreateWordCommandRequest commandRequest, Guid wordId)
    {
        var updatingWord = await _wordRepository.GetByIdAsync(wordId);
        if(updatingWord == null)
            throw new ArgumentException(nameof(wordId));
        
        updatingWord.UpdateDefinition( commandRequest.Definition);
        updatingWord.UpdateValue(commandRequest.Word);
       
        return await _wordRepository.Update(wordId, updatingWord);
    }

    public async Task DeleteWordAsync(Guid wordId)
    {
        await _wordRepository.Delete(wordId);
    }
}