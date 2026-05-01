using TaskProcessing.Service.Domain;

namespace TaskProcessing.Service.Infrastructure.Repositories;

public interface ITaskRepository
{
    Task<Guid> CreateAsync(TaskEntity task);
    Task<TaskEntity?> GetByIdAsync(Guid id);
    Task<List<TaskEntity>> GetListAsync();
    Task UpdateStatusAsync(Guid id, TaskEntityStatus entityStatus);
}