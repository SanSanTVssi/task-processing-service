using TaskProcessing.Service.Domain;

namespace TaskProcessing.Service.Api.Models;


public class UpdateStatusRequest
{
    public TaskEntityStatus? Status { get; set; }
}