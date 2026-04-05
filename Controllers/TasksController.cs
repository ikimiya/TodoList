using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoList.Models;
using TodoList.Services;

namespace TodoList.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class TasksController : ControllerBase
{
    private readonly TasksService _taskService;

    public TasksController(TasksService taskService)
    {
        _taskService = taskService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Tasks>>> GetAll()
    {
        return (await _taskService.GetAllTasks());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Tasks>> GetById(int id)
    {
        var task = await _taskService.GetbyId(id);
        if (task == null)
        {
            return NotFound();
        }
        return task;
    }

    [HttpPost]
    public async Task<IActionResult> Create(Tasks task)
    {
        await _taskService.Create(task);
        return CreatedAtAction(nameof(GetById), new { id = task.Id, task });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Tasks task)
    {
        if (id != task.Id)
        {
            return BadRequest();
        }
        var existingTask = await _taskService.GetbyId(id);
        if (existingTask == null)
        {
            return NotFound();
        }
        await _taskService.Update(task);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var existingTask = await _taskService.GetbyId(id);
        if (existingTask == null)
        {
            return NotFound();
        }
        await _taskService.Delete(id);
        return NoContent();
    }
}
