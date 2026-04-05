using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoList.Models;
using TodoList.Services;
namespace TodoList.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly CategoriesService _categoryService;

    public CategoriesController(CategoriesService categoriesService)
    {
        _categoryService = categoriesService;
    }


    [HttpGet]
    public async Task<ActionResult<List<Categories>>> GetAll()
    {
        return (await _categoryService.GetAllCategories());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Categories>> GetById(int id)
    {
        var categories = await _categoryService.GetById(id);
        if (categories == null)
        {
            return NotFound();
        }
        return categories;
    }

    [HttpPost]
    public async Task<IActionResult> Create(Categories categories)
    {
        await _categoryService.Create(categories);
        return CreatedAtAction(nameof(GetById), new { id = categories.Id }, categories);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Categories categories)
    {
        if (id != categories.Id)
        {
            return BadRequest();
        }

        var existingCategories = await _categoryService.GetById(id);
        if (existingCategories is null)
        {
            return NotFound();
        }

        await _categoryService.Update(categories);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var categories = await _categoryService.GetById(id);

        if (categories is null)
        {
            return NotFound();
        }

        await _categoryService.Delete(id);
        return NoContent();
    }







}

