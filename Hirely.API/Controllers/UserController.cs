using Microsoft.AspNetCore.Mvc;
using Hirely.API.Models.User;
using Hirely.API.Interfaces;

namespace Hirely.API.Controllers
{
  [ApiController]
  [Route("api/[controller]/[action]")]
  public class UserController : ControllerBase
  {
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
      _userService = userService;
    }

    [HttpGet]
    public async Task<ActionResult<List<UserDto>>> GetUsers()
    {
      var users = await _userService.GetListAsync();
      return users;
    }

    [HttpGet]
    public async Task<ActionResult<UserDto>> GetUserById([FromQuery] long userId)
    {
      try
      {
        var user = await _userService.GetByIdAsync(userId);
        return user;
      }
      catch (System.Exception ex)
      {
        return NotFound(ex.Message);
      }
    }

    [HttpPost]
    public async Task<ActionResult<UserDto>> CreateUser([FromBody] CreateUserRequest request)
    {
      try
      {
        var user = await _userService.CreateAsync(request);

        return Created(nameof(CreateUser), user);
      }
      catch (System.Exception ex)
      {
        return BadRequest(ex.Message);
      }
    }

    [HttpPut]
    public async Task<ActionResult<UserDto>> UpdateUser([FromBody] UpdateUserRequest request)
    {
      try
      {
        var user = await _userService.UpdateAsync(request);

        return user;
      }
      catch (System.Exception ex)
      {
        return NotFound(ex.Message);
      }
    }

    [HttpDelete]
    public async Task<ActionResult> DeleteUser([FromQuery] long userId)
    {
      try
      {
        await _userService.DeleteAsync(userId);
        return Ok();
      }
      catch (System.Exception ex)
      {
        return NotFound(ex.Message);
      }
    }
  }
}