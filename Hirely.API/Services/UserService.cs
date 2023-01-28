using Hirely.API.Interfaces;
using Hirely.API.Models.User;
using Hirely.Data;
using Hirely.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Hirely.Common.Exceptions;

namespace Hirely.API.Services
{
  public class UserService : IUserService
  {
    private readonly HirelyDbContext _db;
    public UserService(HirelyDbContext db)
    {
      _db = db;
    }

    public async Task<UserDto> CreateAsync(CreateUserRequest request)
    {
      if (request.Username.Any(x => x == ' '))
      {
        throw new HirelyValidationException(nameof(request.Username), "Username shouldn't contain spaces");
      }

      var existingUser = await _db.Users.FirstOrDefaultAsync(
        x => x.Username.ToUpper() == request.Username.ToUpper()
      );

      if (existingUser != null)
      {
        throw new HirelyValidationException(nameof(request.Username), "This username is already exists");
      }

      var newUser = new User
      {
        Username = request.Username,
        Email = request.Email,
        Password = request.Password,
        FirstName = request.FirstName,
        LastName = request.LastName,
      };

      _db.Users.Add(newUser);

      await _db.SaveChangesAsync();

      var user = await _db.Users
        .Where(x => x.Id == newUser.Id)
        .FirstOrDefaultAsync();

      return MapUserToDto(user);
    }

    public async Task DeleteAsync(long userId)
    {
      var user = await FindUserAsync(userId);

      if (user == null)
      {
        throw new HirelyNotFoundException($"User with Id={userId} is not found");
      }

      _db.Users.Remove(user);

      await _db.SaveChangesAsync();
    }

    public async Task<UserDto> GetByIdAsync(long userId)
    {
      var user = await FindUserAsync(userId);

      if (user == null)
      {
        throw new HirelyNotFoundException($"User with Id={userId} is not found");
      }

      var dto = await _db.Users
          .Where(x => x.Id == userId)
          .Select(x => MapUserToDto(x))
          .FirstOrDefaultAsync();

      return dto;
    }

    public async Task<List<UserDto>> GetListAsync()
    {
      var users = await _db.Users
        .Select(x => MapUserToDto(x))
        .ToListAsync();

      return users;
    }

    public async Task<UserDto> UpdateAsync(UpdateUserRequest request)
    {
      var user = await FindUserAsync(request.Id);

      if (user == null)
      {
        throw new HirelyNotFoundException($"User with Id={request.Id} is not found");
      }

      user.Username = request.Username;
      user.Email = request.Email;
      user.FirstName = request.FirstName;
      user.LastName = request.LastName;

      await _db.SaveChangesAsync();

      var updatedUser = await _db.Users.FirstOrDefaultAsync(x => x.Id == request.Id);

      return MapUserToDto(updatedUser);
    }

    public async Task<User> GetByUsernameAsync(string username)
    {
      var user = await _db.Users.FirstOrDefaultAsync(x => x.Username == username);

      if (user == null)
      {
        throw new HirelyNotFoundException($"User with username {username} is not found");
      }

      return user;
    }

    private async Task<User> FindUserAsync(long userId)
    {
      var user = await _db.Users.FirstOrDefaultAsync(x => x.Id == userId);
      return user;
    }

    private static UserDto MapUserToDto(User user)
    {
      return new UserDto
      {
        Id = user.Id,
        Username = user.Username,
        Email = user.Email,
        FirstName = user.FirstName,
        LastName = user.LastName,
      };
    }
  }
}