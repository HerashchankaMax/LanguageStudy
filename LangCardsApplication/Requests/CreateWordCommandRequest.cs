namespace LangCardsApplication.Requests;

public record CreateWordCommandRequest(string Word, string Definition, string Translation);
