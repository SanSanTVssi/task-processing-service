namespace TaskProcessing.Service.Api.Models;

public class TaskResponse
{
    public Guid Id { get; set; }
    public string? Type { get; set; }
    public string? Status { get; set; }
}