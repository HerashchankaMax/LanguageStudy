namespace LangCardsDomain.Models;

public class FlashCardEntity
{
    public Guid Id { get; private set; }
    public WordEntity OriginalWord { get; private set; }
    public WordEntity TranslatedWord { get; private set; }

    public FlashCardEntity(WordEntity originalWord, WordEntity translatedWord)
    {
        Id = Guid.NewGuid();
        OriginalWord = originalWord;
        TranslatedWord = translatedWord;
    }
}