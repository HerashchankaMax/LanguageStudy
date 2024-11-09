using LangCardsApplication.Requests;
using LangCardsDomain.Exceptions;
using LangCardsDomain.IRepositories;
using LangCardsDomain.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MongoDB.Driver.Linq;

namespace LangCardsApplication;

public class CardsManipulationManager
{
    private readonly IRepository<FlashCardEntity> _cardRepository;
    private readonly IValuableRepository<WordEntity> _wordRepository;
    private readonly IConfiguration _configuration;
    private readonly ILogger<CardsManipulationManager> _logger;

    public CardsManipulationManager(IRepository<FlashCardEntity> cardRepository,
        IValuableRepository<WordEntity> wordRepository,
        IConfiguration configuration,
        ILogger<CardsManipulationManager> logger)
    {
        _cardRepository = cardRepository;
        _wordRepository = wordRepository;
        _configuration = configuration;
        _logger = logger;
    }

    public async Task<IEnumerable<FlashCardEntity>> GetFlashCardsByText(string text)
    {
        var cards = await _cardRepository.GetAllAsync();
        var words = await _wordRepository.FilterByValue(text);
        var result = cards.Where(card => words.Select(w=>w.Id).Contains(card.Id));
        _logger.LogInformation($"Found {result.Count()} flash cards with '{text}' in their content");
        return result;
    }

    public async Task<IEnumerable<FlashCardEntity>> GetCardsAsync() => await _cardRepository.GetAllAsync();
    public async Task<FlashCardEntity?> GetCardByIdAsync(Guid cardId) => await _cardRepository.GetByIdAsync(cardId);

    public async Task<FlashCardEntity?> CreateCardAsync(CreateCardCommandRequest commandRequest)
    {
        var existedWord = await _wordRepository.GetByIdAsync(commandRequest.WordId);
        var wordForSending = existedWord;
        var newCandidate = await _wordRepository.Create(
                new WordEntity(
                    commandRequest.CreateWord.Word,
                    commandRequest.CreateWord.Definition,
                    commandRequest.CreateWord.Translation)
                );
        if (existedWord == null)
        {
            _logger.LogInformation($"Word with value {commandRequest.CreateWord.Word} " +
                                   $"and definition {commandRequest.CreateWord.Definition} is not found and will be created");
            wordForSending = newCandidate;
        }
        else
        {
            if (existedWord != newCandidate)
            {
                return null;
            }
        }
        
        var newCard = new FlashCardEntity(wordForSending);
        _logger.LogInformation($"Created FlashCard with id {newCard.Id} - with word {newCard.WordId}");
        return await _cardRepository.Create(newCard);
    }
    public async Task<FlashCardEntity?> UpdateCardAsync(Guid newWordId, Guid cardId)
    {
        var updatingCard = await _cardRepository.GetByIdAsync(cardId);
        if (updatingCard == null)
        {
            _logger.LogError($"Card updating failed. Card with id {cardId} not found");
        }
        
        var word = await _wordRepository.GetByIdAsync(newWordId);
        if (word == null)
        {
            var name = _configuration.GetSection("FlashCards:DatabaseName").Value ?? "FlashCards database";
            _logger.LogError($"Could not find word with id {newWordId}");
            #if DEBUG
            throw new IdNotFoundException(newWordId, name);
            #endif
        }
        
        updatingCard.UpdateWord(newWordId); 
        var result = await _cardRepository.Update(cardId, updatingCard);
        return result;
    }

    public async Task DeleteCardAsync(Guid cardId)=> await _cardRepository.Delete(cardId);
}