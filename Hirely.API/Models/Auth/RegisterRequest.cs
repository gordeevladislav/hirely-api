using System.ComponentModel.DataAnnotations;

namespace Hirely.API.Models.Auth
{
  public class RegisterRequest
  {
    [Required]
    public string Username { get; set; }
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    public string Password { get; set; }
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
  }
}