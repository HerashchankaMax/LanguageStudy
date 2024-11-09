using LangCardsApplication.Requests;
using LangCardsDomain.Exceptions;
using LangCardsDomain.IRepositories;
using LangCardsDomain.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver.Linq;

namespace LangCardsApplication;

public class CardsManipulationManager
{
    private readonly IRepository<FlashCardEntity> _cardRepository;
    private readonly IValuableRepository<WordEntity> _wordRepository;
    private readonly IConfiguration _configuration;

    public CardsManipulationManager(IRepository<FlashCardEntity> cardRepository,
        IValuableRepository<WordEntity> wordRepository,
        IConfiguration configuration)
    {
        _cardRepository = cardRepository;
        _wordRepository = wordRepository;
        _configuration = configuration;
    }

    public async Task<IEnumerable<FlashCardEntity>> GetFlashCardsByText(string text)
    {
        var cards = await _cardRepository.GetAllAsync();
        var words = await _wordRepository.FilterByValue(text);
        var result = cards.Where(card => words.Select(w=>w.Id).Contains(card.Id));
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
        return await _cardRepository.Create(newCard);
    }
    public async Task<FlashCardEntity?> UpdateCardAsync(Guid newWordId, Guid cardId)
    {
        var updatingCard = await _cardRepository.GetByIdAsync(cardId);
        if(updatingCard == null)
            throw new ArgumentException(nameof(cardId));
        
        var word = await _wordRepository.GetByIdAsync(newWordId);
        if (word == null)
        {
            var name = _configuration.GetSection("FlashCards:DatabaseName").Value ?? "FlashCards database";
            throw new IdNotFoundException(newWordId, name);
        }
        
        updatingCard.UpdateWord(newWordId); 
        return await _cardRepository.Update(cardId, updatingCard);
    }

    public async Task DeleteCardAsync(Guid cardId)
    {
        await _cardRepository.Delete(cardId);
    }
}