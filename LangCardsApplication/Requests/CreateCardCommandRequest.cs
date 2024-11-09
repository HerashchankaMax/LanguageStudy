using LangCardsDomain.Models;

namespace LangCardsApplication.Requests;

public record CreateCardCommandRequest(Guid WordId, CreateWordCommandRequest CreateWord);
