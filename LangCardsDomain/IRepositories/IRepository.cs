namespace LangCardsDomain.IRepositories;

public interface IRepository<T> where T : class
{
    public Task<List<T>> GetAllAsync();
    public Task<T> GetByIdAsync(Guid guid);
    public Task Delete(Guid guid);
}