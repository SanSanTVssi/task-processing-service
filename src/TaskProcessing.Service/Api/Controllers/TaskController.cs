using Microsoft.AspNetCore.Mvc;
using TaskProcessing.Service.Api.Models;
using TaskProcessing.Service.Application;

namespace TaskProcessing.Service.Api.Controllers;

[ApiController]
[Route("tasks")]
public class TaskController(TaskService service) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateAsync(CreateTaskRequest request)
    {
        if (request.Type == null || request.Payload == null)
        {
            return BadRequest();
        }
        var id = await service.CreateAsync(request.Type, request.Payload);
        return Ok(id);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var task = await service.GetByIdAsync(id);

        if (task is null)
        {
            return NotFound();
        }
        
        return Ok(task);
    }

    [HttpGet]
    public async Task<IActionResult> GetList()
    {
        var tasks = await service.GetListAsync();
        return Ok(tasks);
    }

    [HttpPatch("{id}/status")]
    public async Task<IActionResult> UpdateStatus(Guid id, UpdateStatusRequest request)
    {
        if (request.Status is null)
        {
            return BadRequest();
        }
        await service.UpdateStatusAsync(id, request.Status.Value);
        return Ok();
    }
}