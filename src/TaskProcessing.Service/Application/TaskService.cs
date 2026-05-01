using TaskProcessing.Service.Application.Exceptions;
using TaskProcessing.Service.Domain;
using TaskProcessing.Service.Infrastructure.Repositories;

namespace TaskProcessing.Service.Application;

public class TaskService(ITaskRepository repository, ILogger<TaskService> Logger)
{
    public async Task<Guid> CreateAsync(string type, string payload)
    {
        var task = new TaskEntity
        {
            Id = Guid.NewGuid(),
            Type = type,
            Payload = payload,
            EntityStatus = TaskEntityStatus.Created,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        return await repository.CreateAsync(task);
    }

    public Task<TaskEntity?> GetByIdAsync(Guid id)
    {
        return repository.GetByIdAsync(id);
    }

    public Task<List<TaskEntity>> GetListAsync()
    {
        return repository.GetListAsync();
    }
    
    public async Task UpdateStatusAsync(Guid id, TaskEntityStatus newEntityStatus)
    {
        var task = await repository.GetByIdAsync(id);

        if (task == null)
        {
            throw new TaskNotFoundException();
        }
        
        if (task.EntityStatus is TaskEntityStatus.Completed or TaskEntityStatus.Failed)
        {
            throw new InvalidTaskStateTransitionException(from: task.EntityStatus, to: newEntityStatus);
        }

        Logger.LogInformation("Updating task {Guid}, status transition from {TaskEntityStatus}, to {NewEntityStatus}", id, task.EntityStatus, newEntityStatus);
        await repository.UpdateStatusAsync(id, newEntityStatus);
    }
}