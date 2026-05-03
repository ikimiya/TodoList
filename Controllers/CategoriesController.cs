using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoList.Models;
using TodoList.Services;
namespace TodoList.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class CategoriesController : BaseController
{

    private readonly CategoriesService _categoryService;

    public CategoriesController(CategoriesService categoriesService)
    {
        _categoryService = categoriesService;
    }


    [HttpGet]
    public async Task<ActionResult<List<Categories>>> GetAll()
    {
        var userID = GetUserId();
        return await _categoryService.GetAllCategories(userID);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Categories>> GetById(int id)
    {
        var categories = await _categoryService.GetById(id);
        if (categories == null || categories.UserId != GetUserId())
        {
            return NotFound();
        }
        return categories;
    }

    [HttpPost]
    public async Task<IActionResult> Create(Categories categories)
    {
        categories.UserId = GetUserId();
        try
        {
            await _categoryService.Create(categories);
            return CreatedAtAction(nameof(GetById), new { id = categories.Id }, categories);
        }
        catch (Exception ex) 
        {
            return BadRequest(ex.Message);
        }

    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Categories categories)
    {
        var existingCategories = await _categoryService.GetById(id);
        if (existingCategories == null || existingCategories.UserId != GetUserId())
        {
            return NotFound();
        }

        categories.UserId = GetUserId();
        try
        {
            categories.Id = id;
            categories.UserId = GetUserId();
            await _categoryService.Update(categories);
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
        var categories = await _categoryService.GetById(id);

        if (categories == null || categories.UserId != GetUserId())
        {
            return NotFound();
        }

        await _categoryService.Delete(id);
        return NoContent();
    }


}

