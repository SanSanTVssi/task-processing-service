namespace TaskProcessing.Service.Api.Models;

public class CreateTaskRequest
{
    public string? Type { get; set; }
    public string? Payload { get; set; }
}