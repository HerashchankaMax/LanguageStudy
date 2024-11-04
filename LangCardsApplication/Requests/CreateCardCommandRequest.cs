using LangCardsDomain.Models;

namespace LangCardsApplication.Requests;

public record CreateCardCommandRequest(WordEntity OriginalWord, WordEntity TranslatedWord);