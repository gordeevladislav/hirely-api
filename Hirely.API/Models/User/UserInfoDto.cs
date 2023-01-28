namespace Hirely.API.Models.User
{
  public class UserInfoDto
  {
    public long Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Token { get; set; }
  }
}