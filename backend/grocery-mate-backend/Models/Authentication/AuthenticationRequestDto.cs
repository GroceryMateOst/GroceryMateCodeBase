using System.ComponentModel.DataAnnotations;

namespace grocery_mate_backend.Models.Authentication;

public class AuthenticationRequestDto
{
    [Required] public string EmailAddress { get; set; }
    [Required] public string Password { get; set; }
}