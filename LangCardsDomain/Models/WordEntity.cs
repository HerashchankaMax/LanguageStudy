namespace LangCardsDomain.Models;

public class WordEntity
{
   public Guid Id { get; private set; }
   public string Value { get; private set; }
   public string Definition { get; private set; }

   public WordEntity(string word, string definition)
   {
      Id = Guid.NewGuid();
      Value = word;
      Definition = definition;
   }
   public void UpdateValue(string newValue) => Value = newValue;
   public void UpdateDefinition(string newValue) => Definition = newValue;
}