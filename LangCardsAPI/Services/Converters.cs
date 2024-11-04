using LangCardsAPI.Requests;
using LangCardsApplication.Requests;

namespace WebApplication1.Services;

public static class Converters
{
    public static CreateCardCommandRequest ToCreateCommandRequest(this CreateCardRequest request)
        => new CreateCardCommandRequest(request.OriginalWord, request.TranslatedWord);
}