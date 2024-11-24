using LangCardsAPI.Requests;
using LangCardsApplication.Requests;
using LangCardsDomain.Models;

namespace LangCardsAPI.Services;

public static class Converters
{
    public static CreateCardCommandRequest ToCreateCommandRequest(this CreateCardRequest request)
    {
        var createWordCommandRequest = new CreateWordCommandRequest(
            request.CreateWord.Word,
            request.CreateWord.Definition,
            request.CreateWord.Translation);
        return new CreateCardCommandRequest(createWordCommandRequest);
    }

    public static CreateWordCommandRequest ToCreateCommandRequest(this CreateWordRequest request)
    {
        return new CreateWordCommandRequest(request.Word, request.Definition, request.Translation);
    }

    public static CollectionDataCommandRequest ToCreateCommandRequest(this CreateCollectionRequest request)
    {
        var cards = request.Words.Select(c => c.ToCreateCommandRequest()).ToList();
        return new CollectionDataCommandRequest(
            cards,
            request.CollectionName,
            request.Description);
    }

    public static WordEntity ToWordEntity(this CreateWordCommandRequest request)
    {
        return new WordEntity(request.Word, request.Definition, request.Translation);
    }
}