namespace Hirely.API.Models.User
{
  public class UserDto
  {
    public long Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string ProfileImageUrl { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
  }
}