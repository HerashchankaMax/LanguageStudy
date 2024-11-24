namespace LangCardsApplication.Requests;

public record CollectionDataCommandRequest(
    List<CreateWordCommandRequest> Words,
    string CollectionName,
    string Description);