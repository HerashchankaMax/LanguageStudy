using LangCardsApplication.Requests;
using LangCardsDomain.Models;

namespace LangCardsAPI.Requests;

public record CreateCardRequest(Guid WordId, CreateWordCommandRequest CreateWord);
