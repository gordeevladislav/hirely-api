using Hirely.Data.Entities;

namespace Hirely.API.Interfaces
{
  public interface ITokenService
  {
    public string CreateToken(long userId);
  }
}