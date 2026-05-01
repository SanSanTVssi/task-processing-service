namespace TaskProcessing.Service.Domain;

public class TaskEntity
{
    public Guid Id { get; set; }
    public string? Type { get; set; }
    public string? Payload { get; set; }
    public TaskEntityStatus EntityStatus { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}