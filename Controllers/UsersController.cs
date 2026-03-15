using Microsoft.AspNetCore.Mvc;
using TodoList.Models;
using TodoList.Services;
namespace TodoList.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly UserService _userService;

    public UsersController(UserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Users>>> GetAll()
    {
        return (await _userService.GetAllUsers());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Users>> GetById(int id)
    {
        var user = await _userService.GetById(id);
        if (user == null)
        {
            return NotFound();
        }
        return user;
    }

    [HttpPost]
    public async Task<IActionResult> Create(Users user)
    {
        await _userService.Create(user);
        return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Users user)
    {
        if (id != user.Id)
        {
            return BadRequest();
        }

        var existingUser = await _userService.GetById(id);
        if(existingUser is null)
        {
            return NotFound();  
        }

        await _userService.Update(user);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var user = await _userService.GetById(id);

        if(user is null)
        {
            return NotFound();
        }

        await _userService.Delete(id);
        return NoContent();
    }







}
