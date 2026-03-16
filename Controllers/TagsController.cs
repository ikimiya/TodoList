using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoList.Models;
using TodoList.Services;

namespace TodoList.Controllers;
[ApiController]
[Route("[controller]")]
public class TagsController : ControllerBase
{
    private readonly TagsService _tagService;
    
    public TagsController(TagsService tagService)
    {
        _tagService = tagService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Tags>>> GetAll()
    {
        return (await _tagService.GetAllTags());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Tags>> GetById(int id)
    {
        var tag = await _tagService.GetById(id);
        if (tag == null)
        {
            return NotFound();
        }
        return tag;
    }

    [HttpPost]
    public async Task<IActionResult> Create(Tags tag)
    {
        await _tagService.Create(tag);
        return CreatedAtAction(nameof(GetById), new {id = tag.Id},tag);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Tags tag)
    {
        if(id != tag.Id)
        {
            return BadRequest();
        }

        var existTag = await _tagService.GetById(id);
        if(existTag == null)
        {
            return NotFound();
        }

        await _tagService.Update(tag);
        return NoContent();
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var existTag = await _tagService.GetById(id);

        if(existTag == null)
        {
            return NotFound();
        }

        await _tagService.Delete(id);
        return NoContent();

    }   
}
