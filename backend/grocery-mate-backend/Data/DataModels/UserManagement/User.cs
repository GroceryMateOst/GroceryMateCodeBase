using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace grocery_mate_backend.Models;

public class User 
{
    public Guid UserId { get; set; }
    [Required] 
    public string FirstName { get; set; }
    [Required] 
    public string SecondName { get; set; }
    [Required] 
    public string EmailAddress { get; set; }
    [Required] 
    [NotMapped]
    public string Password { get; set; }
    public IdentityUser? Identity { get; set; }

    public User(CreateUserUserDto dto)
    {
        FirstName = dto.FirstName;
        SecondName = dto.SecondName;
        EmailAddress = dto.EmailAddress;
    }
    
    public User(Guid userId, string firstName, string secondName, string emailAddress)
    {
        UserId = userId;
        FirstName = firstName;
        SecondName = secondName;
        EmailAddress = emailAddress;
    }
}
