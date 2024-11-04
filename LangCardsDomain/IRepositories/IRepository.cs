namespace LangCardsDomain.IRepositories;

public interface IRepository<T> where T : class
{
    public Task<T> Create(T item);
    public Task<List<T>> GetAllAsync();
    public Task<T> GetByIdAsync(Guid guid);
    public Task<T> Update(Guid guid, T item);
    public Task Delete(Guid guid);
}