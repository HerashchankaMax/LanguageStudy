namespace LangCardsDomain.Models;

public class CollectionEntity
{
    public string CollectionName { get; set; }
    public List<FlashCardEntity> Cards { get; set; }
    public DateTime ModifiedDate { get; set; }
    public int TotalViews { get; set; }
}