using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoList.Models;
using TodoList.Services;
namespace TodoList.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class UsersController : BaseController
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


    // user profile

    [HttpGet("profile")]
    public async Task<IActionResult> GetProfile()
    {
        var user = await _userService.GetProfile(GetUserId());
        if (user == null) return NotFound();
        return Ok(user);
    }

    [HttpPut("profile")]
    public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileRequest request)
    {
        try
        {
            await _userService.UpdateProfile(GetUserId(), request.Email, request.Password);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    public class UpdateProfileRequest
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

}
