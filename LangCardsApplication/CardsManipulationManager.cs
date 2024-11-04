using LangCardsApplication.Requests;
using LangCardsDomain.Models;
using LangCardsPersistence.Repositories;

namespace LangCardsApplication;

public class CardsManipulationManager
{
    private readonly FlashCardsRepository _cardRepository;

    public CardsManipulationManager(FlashCardsRepository cardRepository)
    {
        _cardRepository = cardRepository;
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
        var newCard = new FlashCardEntity(commandRequest.OriginalWord, commandRequest.TranslatedWord );
        return await _cardRepository.Create(newCard);
    }
    public async Task<FlashCardEntity?> UpdateCardAsync(CreateCardCommandRequest commandRequest, Guid cardId)
    {

        var updatingCard = await _cardRepository.GetByIdAsync(cardId);
        if(updatingCard == null)
            throw new ArgumentException(nameof(cardId));
        
       updatingCard.OriginalWord.UpdateDefinition(commandRequest.OriginalWord.Definition);
       updatingCard.OriginalWord.UpdateValue(commandRequest.OriginalWord.Value);
        
       updatingCard.TranslatedWord.UpdateDefinition(commandRequest.TranslatedWord.Definition);
       updatingCard.TranslatedWord.UpdateValue(commandRequest.TranslatedWord.Value);
       
        return await _cardRepository.Update(cardId, updatingCard);
    }

    public async Task DeleteCardAsync(Guid cardId)
    {
        await _cardRepository.Delete(cardId);
    }
}