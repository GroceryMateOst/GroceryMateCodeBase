using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using grocery_mate_backend.Sandbox;
using Microsoft.AspNetCore.Identity;

namespace grocery_mate_backend.Models;

public class UserDto
{
    [Required] public string FirstName { get; set; }
    [Required] public string SecondName { get; set; }
    [Required] public string EmailAddress { get; set; }
    public string ResidencyDetails { get; set; }

    public UserDto(string firstName, string secondName, string emailAddress, string residencyDetails)
    {
        FirstName = firstName;
        SecondName = secondName;
        EmailAddress = emailAddress;
        ResidencyDetails = residencyDetails;
    }

    public UserDto(string firstName, string secondName, string emailAddress)
    {
        FirstName = firstName;
        SecondName = secondName;
        EmailAddress = emailAddress;
        ResidencyDetails = Symbols.Empty;
    }

    public UserDto()
    {
        FirstName = Symbols.Empty;
        SecondName = Symbols.Empty;
        EmailAddress = Symbols.Empty;
        ResidencyDetails = Symbols.Empty;
    }
}

public class CreateUserDto : UserDto
{
    [Required] public string Password { get; set; }

    public CreateUserDto(string firstName, string secondName, string emailAddress, string residencyDetails,
        string password) : base(firstName, secondName, emailAddress, residencyDetails)
    {
        Password = password;
    }

    public CreateUserDto(string firstName, string secondName, string emailAddress, string password) : base(firstName,
        secondName, emailAddress)
    {
        Password = password;
    }

    public CreateUserDto() : base(Symbols.Empty, Symbols.Empty, Symbols.Empty)
    {
        Password = Symbols.Empty;
    }
}