using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoList.Models;
using TodoList.Services;
namespace TodoList.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class TaskTagsController : ControllerBase
{
    private readonly TaskTagsService _taskTagsService;
    public TaskTagsController(TaskTagsService taskTagsService)
    {
        _taskTagsService = taskTagsService;
    }

    [HttpGet]
    public async Task<ActionResult<List<TaskTags>>> GetAll()
    {
        return (await _taskTagsService.GetAllTaskTags());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TaskTags>> GetById(int id)
    {
        var taskTag = await _taskTagsService.GetById(id);
        if (taskTag == null)
        {
            return NotFound();
        }
        return taskTag;
    }

    [HttpPost]
    public async Task<IActionResult> Create(TaskTags taskTag)
    {
        await _taskTagsService.Create(taskTag);
        return CreatedAtAction(nameof(GetById), new { id = taskTag.TaskId, taskTag });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, TaskTags taskTag)
    {
        if (id != taskTag.TaskId)
        {
            return BadRequest();
        }
        var existingTaskTag = await _taskTagsService.GetById(id);
        if (existingTaskTag == null)
        {
            return NotFound();
        }
        await _taskTagsService.Update(taskTag);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var existingTaskTag = await _taskTagsService.GetById(id);
        if (existingTaskTag == null) 
        {
            return NotFound();
        }
        await _taskTagsService.Delete(id);
        return NoContent();
    }
}
