using TaskProcessing.Service.Application;
using TaskProcessing.Service.Domain;

namespace TaskProcessing.Service.Infrastructure.Repositories;

public class InMemoryTaskRepository(ILogger<TaskService> Logger): ITaskRepository
{
    private readonly Dictionary<Guid, TaskEntity> _storage = new();
    
    public Task<Guid> CreateAsync(TaskEntity task)
    {
        Logger.LogInformation("Creating task {TaskId}", task.Id);
        _storage[task.Id] = task;
        return Task.FromResult(task.Id);
    }

    public Task<TaskEntity?> GetByIdAsync(Guid id)
    {
        return _storage.TryGetValue(id, out var task) 
            ? Task.FromResult<TaskEntity?>(task) 
            : Task.FromResult<TaskEntity?>(null);
    }

    public Task<List<TaskEntity>> GetListAsync()
    {
        return Task.FromResult(_storage.Values.ToList());
    }

    public Task UpdateStatusAsync(Guid id, TaskEntityStatus entityStatus)
    {
        Logger.LogInformation("Updating task {TaskId}", id);
        if (_storage.TryGetValue(id, out var task))
        {
            task.EntityStatus = entityStatus;
            task.UpdatedAt = DateTime.UtcNow;
        }
        
        return Task.CompletedTask;
    }
}