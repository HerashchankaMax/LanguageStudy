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

   public override bool Equals(object? o)
   {
      if (o is null || GetType() != o.GetType())
      {
         return false;
      }

      if (o is WordEntity word)
      {
         return word.Value == Value && word.Definition == Definition && word.Translation == Translation;
      }
      return false;
   }

   public override int GetHashCode()
   {
      return HashCode.Combine(Id, Value, Definition, Translation);
   }

   public static bool operator ==(WordEntity left, WordEntity right)
   {
      return Equals(left, right);
   }

   public static bool operator !=(WordEntity left, WordEntity right)
   {
      return !(left == right);
   }
}