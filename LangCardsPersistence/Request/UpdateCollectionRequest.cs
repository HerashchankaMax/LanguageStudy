using LangCardsDomain.Models;

namespace LangCardsPersistence.Request;

public record UpdateCollectionRequest(
    List<WordEntity> Words,
    string CollectionName,
    string Description);