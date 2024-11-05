using LangCardsApplication.Requests;
using LangCardsDomain.IRepositories;
using LangCardsDomain.Models;

namespace LangCardsApplication;

public class CardsManipulationManager
{
    private readonly IRepository<FlashCardEntity> _cardRepository;
    private readonly IRepository<WordEntity> _wordRepository;

    public CardsManipulationManager(IRepository<FlashCardEntity> cardRepository, IRepository<WordEntity> wordRepository)
    {
        _cardRepository = cardRepository;
        _wordRepository = wordRepository;
    }

    public async Task<IEnumerable<FlashCardEntity>> GetCardsAsync()
    {
        var cards = await _cardRepository.GetAllAsync();
        return cards;
    }

    public async Task<FlashCardEntity?> GetCardByIdAsync(Guid cardId)
    {
        return await _cardRepository.GetByIdAsync(cardId);
    }

    public async Task<FlashCardEntity?> CreateCardAsync(CreateCardCommandRequest commandRequest)
    {
        var existingWord = await _wordRepository.GetByIdAsync(commandRequest.Word.Id);
        var responseWord = commandRequest.Word;
        if (existingWord != null)
        {
            responseWord = existingWord;
        }
        var newCard = new FlashCardEntity(responseWord);
        return await _cardRepository.Create(newCard);
    }
    public async Task<FlashCardEntity?> UpdateCardAsync(CreateCardCommandRequest commandRequest, Guid cardId)
    {
        var updatingCard = await _cardRepository.GetByIdAsync(cardId);
        if(updatingCard == null)
            throw new ArgumentException(nameof(cardId));
        
        updatingCard.UpdateWord(commandRequest.Word); 
       
        return await _cardRepository.Update(cardId, updatingCard);
    }

    public async Task DeleteCardAsync(Guid cardId)
    {
        await _cardRepository.Delete(cardId);
    }
}