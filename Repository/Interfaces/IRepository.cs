namespace Repository.Interfaces;

public interface IRepository<T>
{
    public Task<T> Get(int id);
    public Task<List<T>> GetAllTasks();
    public Task Create(T toCreate);
    public Task Update(T toUpdate);
}