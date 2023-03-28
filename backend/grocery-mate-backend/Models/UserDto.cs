using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace grocery_mate_backend.Models;

public class UserDto
{
    [Required] public string FirstName { get; set; }
    [Required] public string SecondName { get; set; }
    [Required] public string EmailAddress { get; set; }
}

public class CreateUserUserDto : UserDto
{
    [Required] public string Password { get; set; }
}