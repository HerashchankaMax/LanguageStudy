using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LangCardsDomain.Models;

public class WordEntity
{
   [BsonId]
   [BsonRepresentation(BsonType.String)]
   public Guid Id { get; private set; }
   public string Value { get; private set; }
   public string Translation { get; private set; }
   public string Definition { get; private set; }

   public WordEntity(string word, string definition, string translation)
   {
      Id = Guid.NewGuid();
      Value = word;
      Definition = definition;
      Translation = translation;
   }
   public void UpdateValue(string newValue) => Value = newValue;
   public void UpdateDefinition(string newValue) => Definition = newValue;
}