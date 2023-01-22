using System.ComponentModel.DataAnnotations;

namespace Hirely.API.Models.User
{
  public class UpdateUserRequest
  {
    [Range(1, long.MaxValue)]
    public long Id { get; set; }
    public string Username { get; set; }
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    public string ProfileImageUrl { get; set; }
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
  }
}
