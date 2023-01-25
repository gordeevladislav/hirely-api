using Hirely.API.Models.User;
using Hirely.Data.Entities;

namespace Hirely.API.Interfaces
{
  public interface IUserService
  {
    public Task<UserDto> CreateAsync(CreateUserRequest request);
    public Task DeleteAsync(long userId);
    public Task<List<UserDto>> GetListAsync();
    public Task<UserDto> GetByIdAsync(long userId);
    public Task<UserDto> UpdateAsync(UpdateUserRequest request);
    public Task<User> GetByUsernameAsync(string username);
  }
}