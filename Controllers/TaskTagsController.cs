using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoList.Models;
using TodoList.Services;
namespace TodoList.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class TaskTagsController : BaseController
{

    private readonly TaskTagsService _taskTagsService;
    public TaskTagsController(TaskTagsService taskTagsService)
    {
        _taskTagsService = taskTagsService;
    }

    [HttpGet("{taskId}")]
    public async Task<ActionResult<List<TaskTags>>> GetTagsByTaskId(int taskId)
    {
        var userId = GetUserId();

        return (await _taskTagsService.GetTagsByTaskId(taskId,userId));
    }


    [HttpPost("{taskId}/{tagId}")]
    public async Task<IActionResult> Create(int taskId, int tagId)
    {
        var result = await _taskTagsService.Create(taskId, tagId, GetUserId());
        if(!result)
        {
            return NotFound();
        }
        return Ok();
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
    public async Task<IActionResult> Delete(int taskId, int tagId)
    {
        var result = await _taskTagsService.Delete(taskId, tagId, GetUserId());
        if (!result)
        {
            return NotFound();
        }
        return Ok();
    }
}
