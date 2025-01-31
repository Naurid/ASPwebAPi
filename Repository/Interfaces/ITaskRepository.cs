using Repository.Entities;

namespace Repository.Interfaces;

public interface ITaskRepository : IRepository<TaskEntity>
{
    public Task<TaskEntity> GetbyTitle<T>(string title);
    public Task ChangeStatus(int id, string status);
}

