using CRUD_Api.DTO;
using CRUD_Api.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CRUD_Api.Controllers;

[ApiController]
[Route("/")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("users")]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await _userService.GetAll(); 
        return Ok(users);
    }

    [HttpPost("users")]
    public async Task<IActionResult> AddUser([FromBody] UserAddDto user)
    {
        var users = await _userService.AddUser(user);
        if (users.Count == 0)
        {
            return BadRequest("Неправильные данные пользователя");
        }
        return Ok(users);
    }
    
    [HttpPut("users/{id:int}")]
    public async Task<IActionResult> UpdateUser(int id, [FromBody] UserUpdateDto user)
    {
        var users = await _userService.UpdateUser(id, user);
        if (users.Count == 0)
        {
            return BadRequest("Невозможно обновить пользователя");
        }

        return Ok(users);
    }
    
    [HttpDelete("users/{id:int}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var users = await _userService.DeleteUser(id);
        if (users.Count == 0)
        {
            return BadRequest("Пользователь с данным id не найден");
        }

        return Ok(users);
    }
}