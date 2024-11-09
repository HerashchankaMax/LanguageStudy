using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LangCardsDomain.Models;

public class FlashCardEntity
{
   [BsonId]
   [BsonRepresentation(BsonType.String)]
    public Guid Id { get; private set; }
    public Guid WordId { get; private set; }
    public DateTime DateCreated { get; private set; }
    public DateTime DateUpdated { get; private set; }

    public FlashCardEntity(WordEntity originalWord)
    {
        Id = Guid.NewGuid();
        DateCreated = DateTime.Now;
        DateUpdated = DateTime.Now;
        WordId = originalWord.Id;
    }


    public void UpdateWord(Guid originalWordId)
    {
        WordId = originalWordId;
        DateUpdated = DateTime.Now;
    }
}