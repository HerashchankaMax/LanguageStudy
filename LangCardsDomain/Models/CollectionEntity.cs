using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LangCardsDomain.Models;

public class CollectionEntity
{
    public CollectionEntity(string name, List<WordEntity> words)
    {
        Name = name;
        WordGuids = words.Select(w => w.Id).ToList();
        Id = Guid.NewGuid();
    }

    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public Guid Id { get; private set; }

    public string Name { get; private set; }
    public List<Guid>? WordGuids { get; private set; }
    public string Description { get; set; }

    public void AddWord(WordEntity word)
    {
        WordGuids ??= [word.Id];
        WordGuids.Add(word.Id);
    }
}