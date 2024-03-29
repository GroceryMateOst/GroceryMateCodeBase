using System.ComponentModel.DataAnnotations;


namespace grocery_mate_backend.Models;

public class UserDto
{
    public Guid UserId { get; set; }
    [Required] public string FirstName { get; set; }
    [Required] public string SecondName { get; set; }
    [Required] public string EmailAddress { get; set; }
    public string ResidencyDetails { get; set; }

    
    public UserDto(string firstName, string secondName, string emailAddress, string residencyDetails, Guid userId)
    {
        FirstName = firstName;
        SecondName = secondName;
        EmailAddress = emailAddress;
        ResidencyDetails = residencyDetails;
        UserId = userId;
    }
    public UserDto(string firstName, string secondName, string emailAddress, string residencyDetails)
    {
        FirstName = firstName;
        SecondName = secondName;
        EmailAddress = emailAddress;
        ResidencyDetails = residencyDetails;
        UserId = Guid.Empty;
    }

    public UserDto(string firstName, string secondName, string emailAddress)
    {
        FirstName = firstName;
        SecondName = secondName;
        EmailAddress = emailAddress;
        ResidencyDetails = string.Empty;
        UserId = Guid.Empty;
    }

    public UserDto()
    {
        FirstName = string.Empty;
        SecondName = string.Empty;
        EmailAddress = string.Empty;
        ResidencyDetails = string.Empty;
        UserId = Guid.Empty;
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

    public CreateUserDto() : base(string.Empty, string.Empty, string.Empty)
    {
        Password = string.Empty;
    }
}