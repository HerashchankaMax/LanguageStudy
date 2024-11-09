namespace LangCardsDomain.Exceptions;

public class IdNotFoundException : Exception
{
    public IdNotFoundException(Guid id, string dataBaseName) : base($"Id {id} was not found in {dataBaseName} database"){}

}