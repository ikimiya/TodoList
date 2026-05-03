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



    [HttpGet]
    public async Task<ActionResult<List<TaskTags>>> GetAll()
    {
        var userId = GetUserId();
        return (await _taskTagsService.GetAllTaskTags(userId));
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
