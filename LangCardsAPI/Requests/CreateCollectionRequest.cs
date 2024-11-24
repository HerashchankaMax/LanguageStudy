namespace LangCardsAPI.Requests;

public record CreateCollectionRequest(
    Guid CollectionId,
    List<CreateWordRequest> Words,
    string CollectionName,
    string Description);