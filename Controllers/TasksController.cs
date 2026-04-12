using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TodoList.Models;
using TodoList.Services;

namespace TodoList.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class TasksController : ControllerBase
{
    protected int GetUserId()
    {
        return int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
    }

    private readonly TasksService _taskService;

    public TasksController(TasksService taskService)
    {
        _taskService = taskService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Tasks>>> GetAll()
    {
        var userId = GetUserId();
        return await _taskService.GetAllTasks(userId);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Tasks>> GetById(int id)
    {
        var task = await _taskService.GetbyId(id);
        if (task == null || task.UserId != GetUserId())
        {
            return NotFound();
        }
        return task;
    }

    [HttpPost]
    public async Task<IActionResult> Create(Tasks task)
    {
        task.UserId = GetUserId();
        await _taskService.Create(task);
        return CreatedAtAction(nameof(GetById), new { id = task.Id, task });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Tasks task)
    {
        var existingTask = await _taskService.GetbyId(id);
        if (existingTask == null || existingTask.UserId != GetUserId())
        {
            return NotFound();
        }

        task.Id = id;             
        task.UserId = GetUserId();
        await _taskService.Update(task);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var existingTask = await _taskService.GetbyId(id);
        if (existingTask == null || existingTask.UserId != GetUserId())
        {
            return NotFound();
        }
        await _taskService.Delete(id);
        return NoContent();
    }
}
