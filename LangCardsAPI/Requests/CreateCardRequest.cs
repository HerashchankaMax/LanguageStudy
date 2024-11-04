using LangCardsDomain.Models;

namespace LangCardsAPI.Requests;

public record CreateCardRequest(WordEntity OriginalWord, WordEntity TranslatedWord);
