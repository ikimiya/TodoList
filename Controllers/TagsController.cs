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
public class TagsController : BaseController
{

    private readonly TagsService _tagService;
    
    public TagsController(TagsService tagService)
    {
        _tagService = tagService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Tags>>> GetAll()
    {
        var userID = GetUserId();
        return (await _tagService.GetAllTags(userID));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Tags>> GetById(int id)
    {
        var tag = await _tagService.GetById(id);
        if (tag == null || tag.UserId != GetUserId())
        {
            return NotFound();
        }
        return tag;
    }

    [HttpPost]
    public async Task<IActionResult> Create(Tags tag)
    {
        tag.UserId = GetUserId();

        try
        {
            await _tagService.Create(tag);
            return CreatedAtAction(nameof(GetById), new { id = tag.Id }, tag);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }


    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Tags tag)
    {
        var existTag = await _tagService.GetById(id);
        if(existTag == null || existTag.UserId != GetUserId())
        {
            return NotFound();
        }

        tag.UserId = GetUserId();
        try
        {
            tag.Id = id;
            tag.UserId = GetUserId();
            await _tagService.Update(tag);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var existTag = await _tagService.GetById(id);

        if(existTag == null || existTag.UserId != GetUserId())
        {
            return NotFound();
        }

        await _tagService.Delete(id);
        return NoContent();

    }   
}
