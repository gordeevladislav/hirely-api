using Hirely.API.Interfaces;
using Hirely.API.Models.Auth;
using Hirely.API.Models.User;
using Hirely.Common.Exceptions;
using Hirely.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hirely.API.Controllers
{
  [Route("api/[controller]/[action]")]
  [ApiController]
  public class AuthController : ControllerBase
  {
    private readonly ITokenService _tokenService;
    private readonly IUserService _userService;
    private readonly HirelyDbContext _db;
    public AuthController(ITokenService tokenService, IUserService userService, HirelyDbContext db)
    {
      _db = db;
      _tokenService = tokenService;
      _userService = userService;
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<ActionResult<UserInfoDto>> Register(RegisterRequest dto)
    {
      try
      {
        var user = await _userService.CreateAsync(new CreateUserRequest
        {
          Username = dto.Username,
          Email = dto.Email,
          Password = dto.Password,
          FirstName = dto.FirstName,
          LastName = dto.LastName
        });

        var userInfo = new UserInfoDto
        {
          Id = user.Id,
          Username = user.Username,
          Email = user.Email,
          FirstName = user.FirstName,
          LastName = user.LastName,
          Token = _tokenService.CreateToken(user.Id)
        };

        return Created(nameof(Register), userInfo);
      }
      catch (HirelyValidationException ex)
      {
        return BadRequest(ex.Message);
      }
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<ActionResult<UserInfoDto>> SignIn(SignInRequest request)
    {
      try
      {
        var user = await _userService.GetByUsernameAsync(request.Username);
        var isValidPassword = user.Password == request.Password;

        if (isValidPassword == false)
        {
          throw new HirelyValidationException(nameof(request.Password), "Incorrect password");
        }

        return new UserInfoDto
        {
          Id = user.Id,
          Username = user.Username,
          Email = user.Email,
          FirstName = user.FirstName,
          LastName = user.LastName,
          Token = _tokenService.CreateToken(user.Id)
        };
      }
      catch (HirelyNotFoundException ex)
      {
        return NotFound(ex.Message);
      }
      catch (HirelyValidationException ex)
      {
        return BadRequest(ex.Message);
      }
    }
  }
}