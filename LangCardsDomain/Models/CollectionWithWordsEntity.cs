namespace LangCardsDomain.Models;

public class CollectionWithWordsEntity : CollectionEntity
{
    public CollectionWithWordsEntity(string name, List<WordEntity> words) : base(name, words)
    {
        Words = words;
    }

    public List<WordEntity>? Words { get; private set; }
}