namespace LangCardsDomain.IRepositories;

public interface IValuableRepository<T> : IRepository<T> where T : class
{
    public Task<IEnumerable<T>> FilterByValue(string searchTerm);
}