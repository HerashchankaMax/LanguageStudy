using LangCardsAPI.Requests;
using LangCardsApplication.Requests;

namespace LangCardsAPI.Services;

public static class Converters
{
    public static CreateCardCommandRequest ToCreateCommandRequest(this CreateCardRequest request)
    {
        var createWordCommandRequest = new CreateWordCommandRequest(
            request.CreateWord.Word,
            request.CreateWord.Definition,
            request.CreateWord.Translation);
        return new CreateCardCommandRequest(request.WordId, createWordCommandRequest);
    }

    public static CreateWordCommandRequest ToCreateCommandRequest(this CreateWordRequest request)
        => new CreateWordCommandRequest(request.Word, request.Definition, request.Translation);
}