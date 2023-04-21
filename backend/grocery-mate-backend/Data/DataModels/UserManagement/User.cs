using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using grocery_mate_backend.Models;

using Microsoft.AspNetCore.Identity;

namespace grocery_mate_backend.Data.DataModels.UserManagement;

public class User
{
    public Guid UserId { get; set; }
    [Required] public string FirstName { get; set; }
    [Required] public string SecondName { get; set; }
    [Required] public string EmailAddress { get; set; }
    [Required] [NotMapped] public string Password { get; set; }
    public IdentityUser? Identity { get; set; }

    public string? ResidencyDetails { get; set; }

    public Guid? AddressId { get; set; }

    public User(CreateUserDto dto)
    {
        FirstName = dto.FirstName;
        SecondName = dto.SecondName;
        EmailAddress = dto.EmailAddress;
        Password = dto.Password;
    }

    public User(string firstName, string secondName, string emailAddress)
    {
        FirstName = firstName;
        SecondName = secondName;
        EmailAddress = emailAddress;
        Password = string.Empty;
    }

    public User(Guid userId, string firstName, string secondName, string emailAddress, string password,
        IdentityUser? identity, string? residencyDetails, Guid? addressId)
    {
        UserId = userId;
        FirstName = firstName;
        SecondName = secondName;
        EmailAddress = emailAddress;
        Password = password;
        Identity = identity;
        ResidencyDetails = residencyDetails;
        AddressId = addressId;
    }

    public User()
    {
        UserId = Guid.Empty;
        FirstName = string.Empty;
        SecondName = string.Empty;
        EmailAddress = string.Empty;
        Password = string.Empty;
        Identity = new IdentityUser();
        ResidencyDetails = string.Empty;
    }
}